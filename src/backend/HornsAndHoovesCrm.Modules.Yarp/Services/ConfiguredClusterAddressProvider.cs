using HornsAndHoovesCrm.Modules.OpenApi.Services;
using Microsoft.Extensions.Configuration;

namespace HornsAndHoovesCrm.Modules.Yarp.Services;

public class ConfiguredClusterAddressProvider : IClusterAddressProvider
{
    private readonly IConfiguration _configuration;
    private readonly string _configKey;

    public ConfiguredClusterAddressProvider(IConfiguration configuration, string configKey)
    {
        _configuration = configuration;
        _configKey = configKey;
    }

    public IEnumerable<(string Address, string RoutePrefix)> GetClusterAddresses()
    {
        var routesSection = _configuration.GetSection($"{_configKey}:Routes");
        var clustersSection = _configuration.GetSection($"{_configKey}:Clusters");

        foreach (var route in routesSection.GetChildren())
        {
            var clusterId = route.GetValue<string>("ClusterId");
            var path = route.GetSection("Match").GetValue<string>("Path");
            var cluster = clustersSection.GetSection(clusterId);
            // Проверяем, существует ли секция Destinations и содержит ли она дочерние элементы
            var destinations = cluster.GetSection("Destinations").GetChildren();
            var firstDestination = destinations.FirstOrDefault();

            if (firstDestination == null || string.IsNullOrEmpty(path))
            {
                continue;
            }

            var address = firstDestination.GetValue<string>("Address");
            if (!string.IsNullOrEmpty(address))
            {
                var routePrefix = path.Substring(0, path.IndexOf("/{**catch-all}", StringComparison.Ordinal));
                yield return (address, routePrefix);
            }
        }
    }
}