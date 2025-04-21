using HornsAndHoovesCrm.Modules.OpenApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.OpenApi.Writers;

namespace HornsAndHoovesCrm.Modules.OpenApi.Extensions;

public static class EndpointBuilderExtensions
{
    public static void UseCombinedOpenApi(this IEndpointRouteBuilder builder,
        string route = OpenApiConstants.OpenApiCombinedDocumentPath)
    {
        builder.MapGet(route, async (IOpenApiAggregator aggregator) =>
        {
            var doc = await aggregator.GetCombinedOpenApiDocumentAsync();

            var stream = new MemoryStream();
            using var textWriter = new StreamWriter(stream, leaveOpen: true);
            var jsonWriter = new OpenApiJsonWriter(textWriter);
            doc.SerializeAsV3(jsonWriter);
            textWriter.Flush();

            stream.Position = 0;
            return Results.Stream(stream, "application/json");
        });
    }
}