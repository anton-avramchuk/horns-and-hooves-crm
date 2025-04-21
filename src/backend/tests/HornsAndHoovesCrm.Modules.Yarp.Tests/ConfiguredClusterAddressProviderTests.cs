using HornsAndHoovesCrm.Modules.Yarp.Services;
using Microsoft.Extensions.Configuration;

namespace HornsAndHoovesCrm.Modules.Yarp.Tests;

public class ConfiguredClusterAddressProviderTests
{
    [Fact]
    public void GetClusterAddresses_ReturnsAllAddressesAndPrefixesFromConfiguration()
    {
        // Arrange
        var inMemorySettings = new Dictionary<string, string>
        {
            ["Yarp:Routes:route1:ClusterId"] = "cluster1",
            ["Yarp:Routes:route1:Match:Path"] = "/route1/{**catch-all}",
            ["Yarp:Routes:route2:ClusterId"] = "cluster2",
            ["Yarp:Routes:route2:Match:Path"] = "/route2/{**catch-all}",
            ["Yarp:Clusters:cluster1:Destinations:dest1:Address"] = "http://localhost:5001/",
            ["Yarp:Clusters:cluster2:Destinations:dest2:Address"] = "http://localhost:5002/"
        };

        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();

        var provider = new ConfiguredClusterAddressProvider(configuration, "Yarp");

        // Act
        var result = provider.GetClusterAddresses().ToList();

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Contains(result, x => x is { Address: "http://localhost:5001/", RoutePrefix: "/route1" });
        Assert.Contains(result, x => x is { Address: "http://localhost:5002/", RoutePrefix: "/route2" });
    }

    [Fact]
    public void GetClusterAddresses_IgnoresRoutesWithoutValidAddress()
    {
        // Arrange
        var inMemorySettings = new Dictionary<string, string>
        {
            ["Yarp:Routes:route1:ClusterId"] = "cluster1",
            ["Yarp:Routes:route1:Match:Path"] = "/route1/{**catch-all}",
            ["Yarp:Clusters:cluster1:Destinations:dest1:Address"] = "" // Empty address
        };

        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();

        var provider = new ConfiguredClusterAddressProvider(configuration, "Yarp");

        // Act
        var result = provider.GetClusterAddresses().ToList();

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void GetClusterAddresses_IgnoresRoutesWithoutValidPath()
    {
        // Arrange
        var inMemorySettings = new Dictionary<string, string>
        {
            ["Yarp:Routes:route1:ClusterId"] = "cluster1",
            ["Yarp:Routes:route1:Match:Path"] = "", // Empty path
            ["Yarp:Clusters:cluster1:Destinations:dest1:Address"] = "http://localhost:5001/"
        };

        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();

        var provider = new ConfiguredClusterAddressProvider(configuration, "Yarp");

        // Act
        var result = provider.GetClusterAddresses().ToList();

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void GetClusterAddresses_IgnoresClustersWithoutDestinations()
    {
        // Arrange
        var inMemorySettings = new Dictionary<string, string>
        {
            ["Yarp:Routes:route1:ClusterId"] = "cluster1",
            ["Yarp:Routes:route1:Match:Path"] = "/route1/{**catch-all}",
            ["Yarp:Clusters:cluster1:Destinations"] = null // No destinations
        };

        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();

        var provider = new ConfiguredClusterAddressProvider(configuration, "Yarp");

        // Act
        var result = provider.GetClusterAddresses().ToList();

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void GetClusterAddresses_IgnoresNonExistentClusters()
    {
        // Arrange
        var inMemorySettings = new Dictionary<string, string>
        {
            ["Yarp:Routes:route1:ClusterId"] = "nonexistent-cluster",
            ["Yarp:Routes:route1:Match:Path"] = "/route1/{**catch-all}"
        };

        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();

        var provider = new ConfiguredClusterAddressProvider(configuration, "Yarp");

        // Act
        var result = provider.GetClusterAddresses().ToList();

        // Assert
        Assert.Empty(result);
    }
}