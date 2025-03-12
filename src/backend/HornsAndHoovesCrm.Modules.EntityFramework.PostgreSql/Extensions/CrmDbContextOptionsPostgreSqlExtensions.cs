using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;

namespace HornsAndHoovesCrm.Modules.EntityFramework.PostgreSql.Extensions;

public static class CrmDbContextOptionsPostgreSqlExtensions
{
    [Obsolete("Use 'UseNpgsql(...)' method instead. This will be removed in future versions.")]
    public static void UsePostgreSql(
        this CrmDbContextOptions options,
        Action<NpgsqlDbContextOptionsBuilder>? postgreSqlOptionsAction = null)
    {
        options.Configure(context => { context.UseNpgsql(postgreSqlOptionsAction); });
    }

    [Obsolete("Use 'UseNpgsql(...)' method instead. This will be removed in future versions.")]
    public static void UsePostgreSql<TDbContext>(
        this CrmDbContextOptions options,
        Action<NpgsqlDbContextOptionsBuilder>? postgreSqlOptionsAction = null)
        where TDbContext : CrmDbContext<TDbContext>
    {
        options.Configure<TDbContext>(context => { context.UseNpgsql(postgreSqlOptionsAction); });
    }

    public static void UseNpgsql(
        this CrmDbContextOptions options,
        Action<NpgsqlDbContextOptionsBuilder>? postgreSqlOptionsAction = null)
    {
        options.Configure(context => { context.UseNpgsql(postgreSqlOptionsAction); });
    }

    public static void UseNpgsql<TDbContext>(
        this CrmDbContextOptions options,
        Action<NpgsqlDbContextOptionsBuilder>? postgreSqlOptionsAction = null)
        where TDbContext : CrmDbContext<TDbContext>
    {
        options.Configure<TDbContext>(context => { context.UseNpgsql(postgreSqlOptionsAction); });
    }
}