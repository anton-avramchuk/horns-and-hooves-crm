using HornsAndHoovesCrm.AspNetCore.Jwt;
using HornsAndHoovesCrm.AspNetCore.Jwt.Options;
using HornsAndHoovesCrm.AspNetCore.Jwt.Services;
using HornsAndHoovesCrm.Core.Modularity;
using HornsAndHoovesCrm.Modules.Identity.AspNetCore.Jwt.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HornsAndHoovesCrm.Modules.Identity.AspNetCore.Jwt;

[DependsOn(typeof(AspNetCoreJwtModule))]
public class AspNetCoreIdentityJwtModule : CrmModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddScoped<ILoginService<LoginModel>, IdentityLoginService>();
        
        Configure<JwtAuthOptions>(w => { w.AuthPath = "api/auth2/token"; });
    }
}