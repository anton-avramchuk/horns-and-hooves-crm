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

    public IEnumerable<string> GetClusterAddresses()
    {
        return _configuration.GetSection($"{_configKey}:Clusters")
            .GetChildren()
            .Select(cluster => cluster.GetSection("Destinations")
                .GetChildren()
                .First()
                .GetValue<string>("Address"))
            .Where(x => x != null)!;
    }
}