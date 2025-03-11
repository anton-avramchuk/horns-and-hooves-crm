namespace HornsAndHoovesCrm.Core.DataAccess.Abstractions;

public class CrmConnectionStringCheckResult
{
    public bool Connected { get; set; }

    public bool DatabaseExists { get; set; }
}