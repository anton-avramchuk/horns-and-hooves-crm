using HornsAndHoovesCrm.Core.Extensions.DependencyInjection;
using HornsAndHoovesCrm.Core.Modularity;
using HornsAndHoovesCrm.Security;

namespace HornsAndHoovesCrm.AspNetCore;

[DependsOn(typeof(SecurityModule))]
public class AspNetCoreModule : CrmModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpContextAccessor();
        context.Services.AddObjectAccessor<IApplicationBuilder>();
        context.Services.AddObjectAccessor<IEndpointRouteBuilder>();
    }
}