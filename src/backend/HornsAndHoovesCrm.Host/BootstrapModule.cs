using HornsAndHoovesCrm.AspNetCore;
using HornsAndHoovesCrm.AspNetCore.Jwt;
using HornsAndHoovesCrm.Core.Modularity;
using HornsAndHoovesCrm.Modules.OpenApi;
using HornsAndHoovesCrm.Modules.Scalar;

namespace HornsAndHoovesCrm.Host;
[DependsOn(
    typeof(AspNetCoreModule),
    typeof(OpenApiModule),
    typeof(ScalarModule),
    typeof(AspNetCoreJwtModule))]
public class BootstrapModule : CrmModule
{
}