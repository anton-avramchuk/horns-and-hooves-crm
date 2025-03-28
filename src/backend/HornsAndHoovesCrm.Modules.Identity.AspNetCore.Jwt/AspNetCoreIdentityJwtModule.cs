using HornsAndHoovesCrm.AspNetCore.Jwt;
using HornsAndHoovesCrm.Core.Modularity;
using HornsAndHoovesCrm.Modules.Identity.Auth;
using HornsAndHoovesCrm.Modules.Identity.Domain;

namespace HornsAndHoovesCrm.Modules.Identity.AspNetCore.Jwt;

[DependsOn(typeof(AspNetCoreJwtModule), typeof(CrmIdentityModule), typeof(CrmIdentityAuthModule),
    typeof(CrmIdentityDomainModule))]
public class AspNetCoreIdentityJwtModule : CrmModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
    }
}