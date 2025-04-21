using HornsAndHoovesCrm.Modules.Yarp.Services;
using Microsoft.Extensions.Configuration;

namespace HornsAndHoovesCrm.Modules.Yarp.Tests;

public class ConfiguredClusterAddressProviderTests
{
    [Fact]
    public void GetClusterAddresses_ReturnsAllAddressesFromConfiguration()
    {
        // Arrange
        var inMemorySettings = new Dictionary<string, string>
        {
            ["Yarp:Clusters:cluster1:Destinations:dest1:Address"] = "http://localhost:5001/",
            ["Yarp:Clusters:cluster2:Destinations:dest2:Address"] = "http://localhost:5002/"
        };

        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();

        var provider = new ConfiguredClusterAddressProvider(configuration,"Yarp");

        // Act
        var result = provider.GetClusterAddresses().ToList();

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Contains("http://localhost:5001/", result);
        Assert.Contains("http://localhost:5002/", result);
    }
}