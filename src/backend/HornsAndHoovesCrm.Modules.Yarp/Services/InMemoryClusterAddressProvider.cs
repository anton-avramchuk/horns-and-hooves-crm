using HornsAndHoovesCrm.Modules.OpenApi.Services;
using Yarp.ReverseProxy.Configuration;

namespace HornsAndHoovesCrm.Modules.Yarp.Services;

public class InMemoryClusterAddressProvider : IClusterAddressProvider
{
    private readonly IReadOnlyList<ClusterConfig> _clusters;

    public InMemoryClusterAddressProvider(IReadOnlyList<ClusterConfig> clusters)
    {
        _clusters = clusters;
    }

    public IEnumerable<string> GetClusterAddresses()
    {
        foreach (var cluster in _clusters)
        {
            if (cluster.Destinations is null)
                continue;

            foreach (var destination in cluster.Destinations)
            {
                if (Uri.TryCreate(destination.Value.Address, UriKind.Absolute, out var _))
                {
                    yield return destination.Value.Address;
                }
            }
        }
    }
}