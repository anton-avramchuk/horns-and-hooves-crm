using HornsAndHoovesCrm.Core.Extensions.DependencyInjection;
using HornsAndHoovesCrm.Modules.OpenApi.Services;
using HornsAndHoovesCrm.Modules.Yarp.Services;
using Microsoft.Extensions.DependencyInjection;
using Yarp.ReverseProxy.Configuration;

namespace HornsAndHoovesCrm.Modules.Yarp.Extensions;

public static class ServiceCollectionsExtensions
{
    public static IServiceCollection AddYarpFromConfig(this IServiceCollection services,
        string configurationKey = Constants.YarpSectionName)
    {
        var configuration = services.GetConfiguration();

        services.AddSingleton<IClusterAddressProvider>(w =>
            new ConfiguredClusterAddressProvider(configuration, configurationKey));

        var yarpConfig = configuration.GetSection(Constants.YarpSectionName);

        services.AddReverseProxy().LoadFromConfig(yarpConfig);
        return services;
    }


    public static IServiceCollection AddYarpFromConfig(this IServiceCollection services,
        IReadOnlyList<RouteConfig> routes,
        IReadOnlyList<ClusterConfig> clusters)
    {
        
        services.AddSingleton<IClusterAddressProvider>(w=>new InMemoryClusterAddressProvider(clusters));
        
        services.AddReverseProxy().LoadFromMemory(routes, clusters);


        return services;
    }
}