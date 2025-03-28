using HornsAndHoovesCrm.Core.Modularity;
using HornsAndHoovesCrm.Modules.Identity.Domain;

namespace HornsAndHoovesCrm.Modules.Identity.Auth;

[DependsOn(typeof(CrmIdentityModule), typeof(CrmIdentityDomainModule))]
public class CrmIdentityAuthModule : CrmModule
{
}