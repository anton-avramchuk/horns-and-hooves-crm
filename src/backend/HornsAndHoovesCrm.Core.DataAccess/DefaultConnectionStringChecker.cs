using HornsAndHoovesCrm.Core.DataAccess.Abstractions;
using HornsAndHoovesCrm.Core.DependencyInjection;

namespace HornsAndHoovesCrm.Core.DataAccess;

public class DefaultConnectionStringChecker : IConnectionStringChecker, ITransientDependency
{
    public Task<CrmConnectionStringCheckResult> CheckAsync(string connectionString)
    {
        return Task.FromResult(new CrmConnectionStringCheckResult
        {
            Connected = false,
            DatabaseExists = false
        });
    }
}