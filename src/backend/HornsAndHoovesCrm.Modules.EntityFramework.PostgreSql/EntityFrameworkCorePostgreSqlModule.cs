using HornsAndHoovesCrm.Core.Modularity;

namespace HornsAndHoovesCrm.Modules.EntityFramework.PostgreSql;

[DependsOn(typeof(EntityFrameworkModule))]
public class EntityFrameworkCorePostgreSqlModule : CrmModule
{
}