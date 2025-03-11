using HornsAndHoovesCrm.AspNetCore;
using HornsAndHoovesCrm.AspNetCore.Extensions;
using HornsAndHoovesCrm.Core;
using HornsAndHoovesCrm.Core.Modularity;
using Scalar.AspNetCore;

namespace HornsAndHoovesCrm.Modules.Scalar;

[DependsOn(typeof(AspNetCoreModule))]
public class ScalarModule:CrmModule
{
    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var routeBuilder = context.GetRouteBuilder();
        routeBuilder.MapGet("/", context =>
        {
            context.Response.Redirect("/scalar", permanent: false);
            return Task.CompletedTask;
        });
        
        routeBuilder.MapScalarApiReference();
    }
}