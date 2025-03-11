using HornsAndHoovesCrm.Core.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HornsAndHoovesCrm.Core;

public class CrmApplicationCreationOptions
{

    public IServiceCollection Services { get; }



    /// <summary>
    /// The options in this property only take effect when IConfiguration not registered.
    /// </summary>

    public ConfigurationBuilderOptions Configuration { get; }

    public bool SkipConfigureServices { get; set; }

    public string? ApplicationName { get; set; }

    public string? Environment { get; set; }

    public CrmApplicationCreationOptions(IServiceCollection services)
    {
        Services = services;
        Configuration = new ConfigurationBuilderOptions();
    }
}