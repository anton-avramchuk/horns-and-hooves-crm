using Microsoft.AspNetCore.Authorization;

namespace HornsAndHoovesCrm.Authorization.Abstractions;

public class PermissionsRequirement : IAuthorizationRequirement
{
    public string[] PermissionNames { get; }

    public bool RequiresAll { get; }

    public PermissionsRequirement(string[] permissionNames, bool requiresAll)
    {
        PermissionNames = permissionNames;
        RequiresAll = requiresAll;
    }

    public override string ToString()
    {
        return $"PermissionsRequirement: {string.Join(", ", PermissionNames)}";
    }
}