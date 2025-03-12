using HornsAndHoovesCrm.Core.DataAccess;
using HornsAndHoovesCrm.Core.DataAccess.Abstractions;
using HornsAndHoovesCrm.Core.DependencyInjection;
using Npgsql;

namespace HornsAndHoovesCrm.Modules.EntityFramework.PostgreSql;

[Dependency(ReplaceServices = true)]
public class NpgsqlConnectionStringChecker : IConnectionStringChecker, ITransientDependency
{
    public virtual async Task<CrmConnectionStringCheckResult> CheckAsync(string connectionString)
    {
        var result = new CrmConnectionStringCheckResult();
        var connString = new NpgsqlConnectionStringBuilder(connectionString)
        {
            Timeout = 1
        };

        var oldDatabaseName = connString.Database;
        connString.Database = "postgres";

        try
        {
            await using var conn = new NpgsqlConnection(connString.ConnectionString);
            await conn.OpenAsync();
            result.Connected = true;
            await conn.ChangeDatabaseAsync(oldDatabaseName!);
            result.DatabaseExists = true;

            await conn.CloseAsync();

            return result;
        }
        catch (Exception e)
        {
            return result;
        }
    }
}