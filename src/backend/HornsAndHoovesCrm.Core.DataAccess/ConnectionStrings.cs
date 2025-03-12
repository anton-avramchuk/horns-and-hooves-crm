using HornsAndHoovesCrm.Core.Extensions.Collections;

namespace HornsAndHoovesCrm.Core.DataAccess;

public class ConnectionStrings : Dictionary<string, string?>
{
    public const string DefaultConnectionStringName = "Default";

    public string? Default
    {
        get => this.GetOrDefault(DefaultConnectionStringName);
        set => this[DefaultConnectionStringName] = value;
    }
}