using System.Security.Claims;

namespace HornsAndHoovesCrm.Authorization.Abstractions.Permissions;

public class PermissionStateContext
{
    public IServiceProvider ServiceProvider { get; set; } = default!;

    public PermissionDefinition Permission { get; set; } = default!;
}

public class PermissionValueCheckContext
{

    public PermissionDefinition Permission { get; }

    public ClaimsPrincipal? Principal { get; }

    public PermissionValueCheckContext(
        PermissionDefinition permission,
        ClaimsPrincipal? principal)
    {
        Permission = permission;
        Principal = principal;
    }
}