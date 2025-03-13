using HornsAndHoovesCrm.Core.Modularity;
using HornsAndHoovesCrm.Modules.EntityFramework;
using HornsAndHoovesCrm.Modules.Identity.Domain;
using HornsAndHoovesCrm.Security;

namespace HornsAndHoovesCrm.Modules.Identity;

[DependsOn(typeof(CrmIdentityDomainModule), typeof(EntityFrameworkModule), typeof(SecurityModule))]
public class CrmIdentityModule : CrmModule
{
}