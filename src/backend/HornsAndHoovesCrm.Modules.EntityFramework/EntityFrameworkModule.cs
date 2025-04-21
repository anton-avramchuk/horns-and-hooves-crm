using HornsAndHoovesCrm.Core.Modularity;
using HornsAndHoovesCrm.Domain;
using HornsAndHoovesCrm.Modules.EntityFramework.Providers;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace HornsAndHoovesCrm.Modules.EntityFramework;

[DependsOn(typeof(CrmDomainModule))]
public class EntityFrameworkModule : CrmModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.TryAddTransient(typeof(IDbContextProvider<>), typeof(DbContextProvider<>));
    }
}