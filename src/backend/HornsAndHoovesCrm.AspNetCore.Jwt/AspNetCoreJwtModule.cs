using HornsAndHoovesCrm.AspNetCore.Extensions;
using HornsAndHoovesCrm.AspNetCore.Jwt.Extensions;
using HornsAndHoovesCrm.Core;
using HornsAndHoovesCrm.Core.Modularity;

namespace HornsAndHoovesCrm.AspNetCore.Jwt;

[DependsOn(typeof(AspNetCoreModule))]
public class AspNetCoreJwtModule : CrmModule
{
    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var app = context.GetApplicationBuilder();
        app.UseJwtTokenMiddleware();
    }
}