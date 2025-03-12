namespace HornsAndHoovesCrm.Core.DataAccess.Abstractions;

public interface IConnectionStringChecker
{
    Task<CrmConnectionStringCheckResult> CheckAsync(string connectionString);
}

