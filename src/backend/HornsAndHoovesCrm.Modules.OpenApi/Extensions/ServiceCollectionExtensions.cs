using Microsoft.Extensions.DependencyInjection;

namespace HornsAndHoovesCrm.Modules.OpenApi.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddOpenApiClient(this IServiceCollection services)
    {
        services.AddOpenApi();
        return services;
    }
}