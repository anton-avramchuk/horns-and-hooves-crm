using Microsoft.OpenApi.Models;

namespace HornsAndHoovesCrm.Modules.OpenApi.Services;

public interface IOpenApiAggregator
{
    Task<OpenApiDocument> GetCombinedOpenApiDocumentAsync();
}