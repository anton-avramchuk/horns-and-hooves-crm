using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace HornsAndHoovesCrm.Generators.Module;

public static class SymbolExtensions
{
    public static bool IsPartial(this INamedTypeSymbol symbol)
    {
        return symbol.DeclaringSyntaxReferences
            .Select(r => r.GetSyntax())
            .OfType<Microsoft.CodeAnalysis.CSharp.Syntax.ClassDeclarationSyntax>()
            .Any(c => c.Modifiers.Any(m => m.IsKind(SyntaxKind.PartialKeyword)));
    }

    public static ImmutableArray<INamedTypeSymbol> GetTypesWithAttribute(this IAssemblySymbol assembly,
        string attributeName)
    {
        var results = ImmutableArray.CreateBuilder<INamedTypeSymbol>();

        // Рекурсивная функция для обхода пространства имен
        void SearchNamespace(INamespaceSymbol namespaceSymbol)
        {
            // Ищем типы в текущем пространстве имен
            foreach (var type in namespaceSymbol.GetTypeMembers())
            {
                if (type.GetAttributes().Any(a => a.AttributeClass?.ToDisplayString() == attributeName))
                {
                    results.Add(type);
                }

                // Рекурсивно обходим вложенные типы
                foreach (var nestedType in type.GetTypeMembers())
                {
                    if (nestedType.GetAttributes().Any(a => a.AttributeClass?.ToDisplayString() == attributeName))
                    {
                        results.Add(nestedType);
                    }
                }
            }

            // Рекурсивно обходим вложенные пространства имен
            foreach (var nestedNamespace in namespaceSymbol.GetNamespaceMembers())
            {
                SearchNamespace(nestedNamespace);
            }
        }

        // Запускаем поиск с глобального пространства имен
        SearchNamespace(assembly.GlobalNamespace);

        return results.ToImmutable();
    }
}