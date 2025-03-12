using HornsAndHoovesCrm.Core.Modularity;
using HornsAndHoovesCrm.Modules.EntityFramework.Providers;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace HornsAndHoovesCrm.Modules.EntityFramework;

public class EntityFrameworkModule : CrmModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.TryAddTransient(typeof(IDbContextProvider<>), typeof(DbContextProvider<>));
    }
}