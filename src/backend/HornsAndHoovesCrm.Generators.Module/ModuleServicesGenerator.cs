using System;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace HornsAndHoovesCrm.Generators.Module;

[Generator]
public class ModuleServicesGenerator : IIncrementalGenerator
{
    private const string ErrorCode = "MODGEN001";
    private static string ErrorCategory = nameof(ModuleServicesGenerator);

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        // Находим все классы, реализующие IModule
        var modules = context.SyntaxProvider
            .CreateSyntaxProvider(
                predicate: static (node, _) => IsClassDeclaration(node),
                transform: static (context, _) => GetModuleIfImplementsIModule(context))
            .Where(static module => module is not null)
            .Collect();

        context.RegisterSourceOutput(modules, static (ctx, moduleSymbols) =>
        {
            if (moduleSymbols.IsDefaultOrEmpty)
            {
                ctx.AddError(ErrorCode, "Не найдено классов, реализующих IModule.", ErrorCategory);
                return;
            }

            if (moduleSymbols.Length > 1)
            {
                ctx.AddError(ErrorCode, "Найдено несколько классов, реализующих IModule. Ожидается ровно один.",
                    ErrorCategory);
                return;
            }

            var moduleSymbol = moduleSymbols[0];

            if (!moduleSymbol.IsPartial())
            {
                ctx.AddError(ErrorCode, $"Класс {moduleSymbol.Name} должен быть partial.", ErrorCategory);
                return;
            }

            GenerateModuleCode(ctx, moduleSymbol);
        });
    }

    private static void GenerateModuleCode(SourceProductionContext ctx, INamedTypeSymbol moduleSymbol)
    {
        var servicesRegistration = GenerateServicesRegistration(moduleSymbol);

        var code = $@"
using Microsoft.Extensions.DependencyInjection;

namespace {moduleSymbol.ContainingNamespace}
{{
    public partial class {moduleSymbol.Name}
    {{
        public void RegisterServices(IServiceCollection services)
        {{
            {servicesRegistration}
        }}
    }}
}}";

        ctx.AddSource($"{moduleSymbol.Name}_Generated.g.cs", SourceText.From(code, Encoding.UTF8));
    }


    private static string GenerateServicesRegistration(INamedTypeSymbol moduleSymbol)
    {
        var compilation = moduleSymbol.ContainingAssembly;
        var classesWithExport = compilation.GetTypesWithAttribute(Constants.ExportAttributeName);
        var registrations = new StringBuilder();

        foreach (var classSymbol in classesWithExport)
        {
            foreach (var attribute in classSymbol.GetAttributes()
                         .Where(a => a.AttributeClass?.ToDisplayString() == Constants.ExportAttributeName))
            {
                var exportType = (ExportType)attribute.ConstructorArguments[0].Value;
                var exportedTypes = attribute.ConstructorArguments[1].Values;

                var registrationMethod = exportType switch
                {
                    ExportType.Scope => "AddScoped",
                    ExportType.Trancient => "AddTransient",
                    ExportType.Single => "AddSingleton",
                    _ => throw new NotImplementedException(),
                };

                string implementationType = GetTypeName(classSymbol);

                if (exportedTypes.Length == 0)
                {
                    registrations.AppendLine($"services.{registrationMethod}(typeof({implementationType}));");
                }
                else
                {
                    foreach (var exportedType in exportedTypes)
                    {
                        var serviceType = exportedType.Value?.ToString();
                        if (!string.IsNullOrEmpty(serviceType))
                        {
                            registrations.AppendLine(
                                $"services.{registrationMethod}(typeof({serviceType}), typeof({implementationType}));");
                        }
                        else
                        {
                            registrations.AppendLine($"services.{registrationMethod}(typeof({implementationType}));");
                        }
                    }
                }
            }
        }

        return registrations.ToString();
    }

    /// <summary>
    /// Генерирует строковое представление типа, включая поддержку дженериков.
    /// </summary>
    private static string GetTypeName(INamedTypeSymbol typeSymbol)
    {
        if (typeSymbol.TypeParameters.Length == 0)
        {
            return typeSymbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
        }

        string baseName = typeSymbol.ContainingNamespace.IsGlobalNamespace
            ? typeSymbol.Name
            : $"{typeSymbol.ContainingNamespace}.{typeSymbol.Name}";

        string genericParams = new string(',', typeSymbol.TypeParameters.Length - 1);

        return $"{baseName}<{genericParams}>";
    }


    private static bool IsClassDeclaration(SyntaxNode node)
    {
        return node is ClassDeclarationSyntax;
    }

    private static INamedTypeSymbol? GetModuleIfImplementsIModule(GeneratorSyntaxContext context)
    {
        var classDeclaration = (ClassDeclarationSyntax)context.Node;
        var semanticModel = context.SemanticModel;

        var classSymbol = ModelExtensions.GetDeclaredSymbol(semanticModel, classDeclaration);
        if (classSymbol is null)
            return null;

        
        if (!classSymbol.AllInterfaces.Any(i => i.ToDisplayString() == Constants.ModuleTypeName))
            return null;

        return classSymbol;
    }
}