using HornsAndHoovesCrm.Core.DependencyInjection;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Readers;

namespace HornsAndHoovesCrm.Modules.OpenApi.Services;

public class OpenApiAggregator : IOpenApiAggregator, ISingletonDependency
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly IClusterAddressProvider _addressProvider;

    public OpenApiAggregator(IHttpClientFactory clientFactory, IClusterAddressProvider addressProvider)
    {
        _clientFactory = clientFactory;
        _addressProvider = addressProvider;
    }

    public async Task<OpenApiDocument> GetCombinedOpenApiDocumentAsync()
    {
        var clusterAddresses = _addressProvider.GetClusterAddresses();

        var reader = new OpenApiStringReader();
        var combined = new OpenApiDocument
        {
            Info = new OpenApiInfo { Title = "Combined API", Version = "v1" },
            Paths = new OpenApiPaths()
        };

        foreach (var address in clusterAddresses)
        {
            try
            {
                var fullUri = new Uri(new Uri(address), "openapi/v1.json");
                var json = await _clientFactory.CreateClient().GetStringAsync(fullUri);
                var doc = reader.Read(json, out var diagnostic);

                foreach (var path in doc.Paths)
                {
                    combined.Paths[path.Key] = path.Value;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при обработке {address}: {ex.Message}");
            }
        }

        return combined;
    }
}