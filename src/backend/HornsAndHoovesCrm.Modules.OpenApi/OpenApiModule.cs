using HornsAndHoovesCrm.AspNetCore;
using HornsAndHoovesCrm.AspNetCore.Extensions;
using HornsAndHoovesCrm.Core;
using HornsAndHoovesCrm.Core.Modularity;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace HornsAndHoovesCrm.Modules.OpenApi;

[DependsOn(typeof(AspNetCoreModule))]
public class OpenApiModule : CrmModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddOpenApi();
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var routeBuilder = context.GetRouteBuilder();
        routeBuilder.MapOpenApi();
    }
}