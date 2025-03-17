using HornsAndHoovesCrm.Authorization.Abstractions.Permissions;

namespace HornsAndHoovesCrm.Authorization.Permissions;

public interface IDynamicPermissionDefinitionStore
{
    Task<PermissionDefinition?> GetOrNullAsync(string name);

    Task<IReadOnlyList<PermissionDefinition>> GetPermissionsAsync();

    Task<IReadOnlyList<PermissionGroupDefinition>> GetGroupsAsync();
}