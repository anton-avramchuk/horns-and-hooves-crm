using HornsAndHoovesCrm.AspNetCore;
using HornsAndHoovesCrm.AspNetCore.Jwt;
using HornsAndHoovesCrm.AspNetCore.Jwt.Extensions;
using HornsAndHoovesCrm.Core;
using HornsAndHoovesCrm.Core.Modularity;
using HornsAndHoovesCrm.Modules.Identity.AspNetCore.Jwt;
using HornsAndHoovesCrm.Modules.Identity.AspNetCore.Jwt.Services;
using HornsAndHoovesCrm.Modules.OpenApi;
using HornsAndHoovesCrm.Modules.Scalar;

namespace HornsAndHoovesCrm.Host;

[DependsOn(
    typeof(AspNetCoreModule),
    typeof(OpenApiModule),
    typeof(ScalarModule),
    typeof(AspNetCoreJwtModule),
    typeof(AspNetCoreIdentityJwtModule)
)]
public class BootstrapModule : CrmModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddJwt("HornsAndHoovesCrm", "HornsAndHoovesCrm");
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        context.UseJwtAuthModel<LoginModel>();
    }
}