using HornsAndHoovesCrm.AspNetCore;
using HornsAndHoovesCrm.Core.Modularity;

namespace HornsAndHoovesCrm.Modules.Identity.AspNetCore;

[DependsOn(typeof(AspNetCoreModule))]
public class AspNetCoreIdentityModule : CrmModule
{
}