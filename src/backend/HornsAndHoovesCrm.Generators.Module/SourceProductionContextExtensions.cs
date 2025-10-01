using Microsoft.CodeAnalysis;

namespace HornsAndHoovesCrm.Generators.Module;

public static class SourceProductionContextExtensions
{
    public static void AddError(this SourceProductionContext context, string code, string message, string category)
    {
        var diagnostic = Diagnostic.Create(new DiagnosticDescriptor(
                id: code,
                title: message,
                messageFormat: message,
                category: category,
                DiagnosticSeverity.Error,
                isEnabledByDefault: true),
            Location.None);

        context.ReportDiagnostic(diagnostic);
    }
}