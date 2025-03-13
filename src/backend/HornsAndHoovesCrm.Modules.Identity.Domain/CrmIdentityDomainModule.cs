using HornsAndHoovesCrm.Core.Modularity;
using HornsAndHoovesCrm.Domain;

namespace HornsAndHoovesCrm.Modules.Identity.Domain;

[DependsOn(typeof(CrmDomainModule))]
public class CrmIdentityDomainModule : CrmModule
{
}