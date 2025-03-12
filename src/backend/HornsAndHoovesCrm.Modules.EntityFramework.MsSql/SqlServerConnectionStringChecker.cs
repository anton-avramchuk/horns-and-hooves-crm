using HornsAndHoovesCrm.Core.DataAccess;
using HornsAndHoovesCrm.Core.DataAccess.Abstractions;
using HornsAndHoovesCrm.Core.DependencyInjection;
using Microsoft.Data.SqlClient;

namespace HornsAndHoovesCrm.Modules.EntityFramework.MsSql;

[Dependency(ReplaceServices = true)]
public class SqlServerConnectionStringChecker : IConnectionStringChecker, ITransientDependency
{
    public virtual async Task<CrmConnectionStringCheckResult> CheckAsync(string connectionString)
    {
        var result = new CrmConnectionStringCheckResult();
        var connString = new SqlConnectionStringBuilder(connectionString)
        {
            ConnectTimeout = 1
        };

        var oldDatabaseName = connString.InitialCatalog;
        connString.InitialCatalog = "master";

        try
        {
            await using var conn = new SqlConnection(connString.ConnectionString);
            await conn.OpenAsync();
            result.Connected = true;
            await conn.ChangeDatabaseAsync(oldDatabaseName);
            result.DatabaseExists = true;

            await conn.CloseAsync();

            return result;
        }
        catch (Exception)
        {
            return result;
        }
    }
}