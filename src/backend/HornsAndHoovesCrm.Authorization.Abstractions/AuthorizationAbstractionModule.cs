using HornsAndHoovesCrm.Core.Modularity;
using HornsAndHoovesCrm.Security;

namespace HornsAndHoovesCrm.Authorization.Abstractions;

[DependsOn(typeof(SecurityModule))]
public class AuthorizationAbstractionModule : CrmModule
{
}