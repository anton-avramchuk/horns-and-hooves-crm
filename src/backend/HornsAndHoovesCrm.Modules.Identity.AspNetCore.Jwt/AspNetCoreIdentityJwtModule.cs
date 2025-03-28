using HornsAndHoovesCrm.AspNetCore.Jwt;
using HornsAndHoovesCrm.Core.Modularity;
using HornsAndHoovesCrm.Modules.Identity.AspNetCore.Jwt.Options;
using HornsAndHoovesCrm.Modules.Identity.Auth;
using HornsAndHoovesCrm.Modules.Identity.Domain;
using HornsAndHoovesCrm.Security;

namespace HornsAndHoovesCrm.Modules.Identity.AspNetCore.Jwt;

[DependsOn(typeof(AspNetCoreJwtModule), typeof(CrmIdentityModule), typeof(CrmIdentityAuthModule),
    typeof(CrmIdentityDomainModule), typeof(SecurityModule))]
public class AspNetCoreIdentityJwtModule : CrmModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<JwtConfiguration>(w =>
        {
            w.TokenLifetimeMinutes = 24 * 60;
            w.Secret = "12345678";
        });
    }
}