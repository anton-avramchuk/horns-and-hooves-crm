using HornsAndHoovesCrm.Modules.Yarp.Services;
using Yarp.ReverseProxy.Configuration;

namespace HornsAndHoovesCrm.Modules.Yarp.Tests;

public class InMemoryClusterAddressProviderTests
{
    [Fact]
    public void GetClusterAddresses_ReturnsAddressesFromClusterConfig()
    {
        // Arrange
        var clusters = new List<ClusterConfig>
        {
            new ClusterConfig
            {
                ClusterId = "cluster1",
                Destinations = new Dictionary<string, DestinationConfig>
                {
                    { "d1", new DestinationConfig { Address = "http://localhost:6001/" } }
                }
            },
            new ClusterConfig
            {
                ClusterId = "cluster2",
                Destinations = new Dictionary<string, DestinationConfig>
                {
                    { "d2", new DestinationConfig { Address = "http://localhost:6002/" } }
                }
            }
        };

        var provider = new InMemoryClusterAddressProvider(clusters);

        // Act
        var result = provider.GetClusterAddresses().ToList();

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Contains("http://localhost:6001/", result);
        Assert.Contains("http://localhost:6002/", result);
    }

    [Fact]
    public void GetClusterAddresses_IgnoresClustersWithoutDestinations()
    {
        // Arrange
        var clusters = new List<ClusterConfig>
        {
            new()
            {
                ClusterId = "cluster1",
                Destinations = null // intentionally null
            }
        };

        var provider = new InMemoryClusterAddressProvider(clusters);

        // Act
        var result = provider.GetClusterAddresses().ToList();

        // Assert
        Assert.Empty(result);
    }
}