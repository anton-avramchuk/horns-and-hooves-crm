using HornsAndHoovesCrm.Modules.Yarp.Services;
using Yarp.ReverseProxy.Configuration;

public class InMemoryClusterAddressProviderTests
{
    [Fact]
    public void GetClusterAddressesWithRoutes_ReturnsAddressesAndPrefixesFromConfigs()
    {
        // Arrange
        var routes = new List<RouteConfig>
        {
            new RouteConfig
            {
                ClusterId = "cluster1",
                Match = new RouteMatch { Path = "/route1/{**catch-all}" }
            },
            new RouteConfig
            {
                ClusterId = "cluster2",
                Match = new RouteMatch { Path = "/route2/{**catch-all}" }
            }
        };

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

        var provider = new InMemoryClusterAddressProvider(routes, clusters);

        // Act
        var result = provider.GetClusterAddresses().ToList();

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Contains(result, x => x is { Address: "http://localhost:6001/", RoutePrefix: "/route1" });
        Assert.Contains(result, x => x is { Address: "http://localhost:6002/", RoutePrefix: "/route2" });
    }

    [Fact]
    public void GetClusterAddressesWithRoutes_IgnoresClustersWithoutDestinations()
    {
        // Arrange
        var routes = new List<RouteConfig>
        {
            new RouteConfig
            {
                ClusterId = "cluster1",
                Match = new RouteMatch { Path = "/route1/{**catch-all}" }
            }
        };

        var clusters = new List<ClusterConfig>
        {
            new ClusterConfig
            {
                ClusterId = "cluster1",
                Destinations = null // intentionally null
            }
        };

        var provider = new InMemoryClusterAddressProvider(routes, clusters);

        // Act
        var result = provider.GetClusterAddresses().ToList();

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void GetClusterAddressesWithRoutes_IgnoresRoutesWithoutValidPath()
    {
        // Arrange
        var routes = new List<RouteConfig>
        {
            new RouteConfig
            {
                ClusterId = "cluster1",
                Match = new RouteMatch { Path = null } // invalid path
            }
        };

        var clusters = new List<ClusterConfig>
        {
            new ClusterConfig
            {
                ClusterId = "cluster1",
                Destinations = new Dictionary<string, DestinationConfig>
                {
                    { "d1", new DestinationConfig { Address = "http://localhost:6001/" } }
                }
            }
        };

        var provider = new InMemoryClusterAddressProvider(routes, clusters);

        // Act
        var result = provider.GetClusterAddresses().ToList();

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void GetClusterAddressesWithRoutes_IgnoresInvalidAddresses()
    {
        // Arrange
        var routes = new List<RouteConfig>
        {
            new RouteConfig
            {
                ClusterId = "cluster1",
                Match = new RouteMatch { Path = "/route1/{**catch-all}" }
            }
        };

        var clusters = new List<ClusterConfig>
        {
            new ClusterConfig
            {
                ClusterId = "cluster1",
                Destinations = new Dictionary<string, DestinationConfig>
                {
                    { "d1", new DestinationConfig { Address = "invalid-url" } } // invalid URI
                }
            }
        };

        var provider = new InMemoryClusterAddressProvider(routes, clusters);

        // Act
        var result = provider.GetClusterAddresses().ToList();

        // Assert
        Assert.Empty(result);
    }
}