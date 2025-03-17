using System.Security.Claims;

namespace HornsAndHoovesCrm.Authorization.Abstractions.Permissions;

public class PermissionValuesCheckContext
{

    public List<PermissionDefinition> Permissions { get; }

    public ClaimsPrincipal? Principal { get; }

    public PermissionValuesCheckContext(
        List<PermissionDefinition> permissions,
        ClaimsPrincipal? principal)
    {
        Permissions = permissions;
        Principal = principal;
    }
}