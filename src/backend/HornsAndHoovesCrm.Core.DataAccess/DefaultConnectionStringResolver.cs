using HornsAndHoovesCrm.Core.DataAccess.Abstractions;
using HornsAndHoovesCrm.Core.DependencyInjection;
using Microsoft.Extensions.Options;
using HornsAndHoovesCrm.Core.Extensions.Common;

namespace HornsAndHoovesCrm.Core.DataAccess;

public class DefaultConnectionStringResolver : IConnectionStringResolver, ITransientDependency
{
    protected CrmDbConnectionOptions Options { get; }

    public DefaultConnectionStringResolver(
        IOptionsMonitor<CrmDbConnectionOptions> options)
    {
        Options = options.CurrentValue;
    }

    [Obsolete("Use ResolveAsync method.")]
    public virtual string Resolve(string? connectionStringName = null)
    {
        return ResolveInternal(connectionStringName)!;
    }

    public virtual Task<string> ResolveAsync(string? connectionStringName = null)
    {
        return Task.FromResult(ResolveInternal(connectionStringName))!;
    }

    private string? ResolveInternal(string? connectionStringName)
    {
        if (connectionStringName == null)
        {
            return Options.ConnectionStrings.Default;
        }

        var connectionString = Options.GetConnectionStringOrNull(connectionStringName);

        if (!connectionString.IsNullOrEmpty())
        {
            return connectionString;
        }

        return null;
    }
}