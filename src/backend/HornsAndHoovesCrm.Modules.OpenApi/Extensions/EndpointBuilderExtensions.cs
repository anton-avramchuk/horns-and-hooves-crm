using HornsAndHoovesCrm.Modules.OpenApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Readers;

namespace HornsAndHoovesCrm.Modules.OpenApi.Extensions;

public static class EndpointBuilderExtensions
{
    public static IEndpointRouteBuilder UseCombinedOpenApi(this IEndpointRouteBuilder builder)
    {
        builder.MapGet("/openapi/combined", async (IOpenApiAggregator aggregator) =>
        {
            var doc = await aggregator.GetCombinedOpenApiDocumentAsync();

            var stream = new MemoryStream();
            using var textWriter = new StreamWriter(stream);
            var jsonWriter = new Microsoft.OpenApi.Writers.OpenApiJsonWriter(textWriter);
            doc.SerializeAsV3(jsonWriter);
            textWriter.Flush();

            stream.Position = 0;
            return Results.Stream(stream, "application/json");
        });
        return builder;
    }
}