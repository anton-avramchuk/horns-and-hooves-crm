using HornsAndHoovesCrm.Core.Modularity;

namespace HornsAndHoovesCrm.Modules.EntityFramework.MsSql;

[DependsOn(typeof(EntityFrameworkModule))]
public class EntityFrameworkCoreSqlServerModule : CrmModule
{
}