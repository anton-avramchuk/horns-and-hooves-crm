using Microsoft.Extensions.DependencyInjection;

namespace HornsAndHoovesCrm.OpenApi.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddOpenApiClient(this IServiceCollection services)
    {
        services.AddOpenApi();
        return services;
    }
}