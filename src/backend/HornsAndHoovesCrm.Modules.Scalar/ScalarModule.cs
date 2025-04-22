using HornsAndHoovesCrm.AspNetCore;
using HornsAndHoovesCrm.AspNetCore.Extensions;
using HornsAndHoovesCrm.Core;
using HornsAndHoovesCrm.Core.Modularity;
using Scalar.AspNetCore;

namespace HornsAndHoovesCrm.Modules.Scalar;

[DependsOn(typeof(AspNetCoreModule))]
public class ScalarModule : CrmModule
{
    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var routeBuilder = context.GetRouteBuilder();
        routeBuilder.MapGet("/", httpContext =>
        {
            httpContext.Response.Redirect("/scalar", permanent: false);
            return Task.CompletedTask;
        });


        var options = context.GetOptions<ScalarModuleOptions>();


        routeBuilder.MapScalarApiReference(w =>
        {
            if (!string.IsNullOrWhiteSpace(options.OpenApiPath))
            {
                w.OpenApiRoutePattern = options.OpenApiPath;
            }
        });
    }
}