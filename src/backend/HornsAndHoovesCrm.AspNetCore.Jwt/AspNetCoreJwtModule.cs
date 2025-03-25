using HornsAndHoovesCrm.AspNetCore.Extensions;
using HornsAndHoovesCrm.AspNetCore.Jwt.Extensions;
using HornsAndHoovesCrm.AspNetCore.Jwt.Options;
using HornsAndHoovesCrm.Core;
using HornsAndHoovesCrm.Core.Modularity;

namespace HornsAndHoovesCrm.AspNetCore.Jwt;

[DependsOn(typeof(AspNetCoreModule))]
public class AspNetCoreJwtModule : CrmModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<JwtAuthOptions>(w => { w.AuthPath = "api/auth/token"; });
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var app = context.GetApplicationBuilder();
        app.UseJwtTokenMiddleware();

        
    }
}