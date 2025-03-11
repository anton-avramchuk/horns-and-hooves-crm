using HornsAndHoovesCrm.AspNetCore;
using HornsAndHoovesCrm.Core.Modularity;
using HornsAndHoovesCrm.Modules.OpenApi;
using HornsAndHoovesCrm.Modules.Scalar;

namespace HornsAndHoovesCrm.Host;
[DependsOn(typeof(AspNetCoreModule),typeof(OpenApiModule),typeof(ScalarModule))]
public class BootstrapModule : CrmModule
{
}