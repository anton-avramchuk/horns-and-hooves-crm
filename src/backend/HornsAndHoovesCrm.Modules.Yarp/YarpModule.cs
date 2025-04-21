using HornsAndHoovesCrm.AspNetCore;
using HornsAndHoovesCrm.AspNetCore.Extensions;
using HornsAndHoovesCrm.Core;
using HornsAndHoovesCrm.Core.Extensions.DependencyInjection;
using HornsAndHoovesCrm.Core.Modularity;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace HornsAndHoovesCrm.Modules.Yarp;

[DependsOn(typeof(AspNetCoreModule))]
public class YarpModule:CrmModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();
        var yarpConfig = configuration.GetSection("Yarp");

        context.Services.AddReverseProxy().LoadFromConfig(yarpConfig);
    }


    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var routeBuilder = context.GetRouteBuilder();
        routeBuilder.MapReverseProxy();
    }
}