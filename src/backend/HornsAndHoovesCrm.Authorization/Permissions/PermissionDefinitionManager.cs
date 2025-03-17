using System.Collections.Immutable;
using HornsAndHoovesCrm.Authorization.Abstractions.Permissions;
using HornsAndHoovesCrm.Core.DependencyInjection;
using HornsAndHoovesCrm.Core.Exceptions;

namespace HornsAndHoovesCrm.Authorization.Permissions;

public class PermissionDefinitionManager : IPermissionDefinitionManager, ITransientDependency
{
    private readonly IStaticPermissionDefinitionStore _staticStore;
    private readonly IDynamicPermissionDefinitionStore _dynamicStore;

    public PermissionDefinitionManager(
        IStaticPermissionDefinitionStore staticStore,
        IDynamicPermissionDefinitionStore dynamicStore)
    {
        _staticStore = staticStore;
        _dynamicStore = dynamicStore;
    }

    public virtual async Task<PermissionDefinition> GetAsync(string name)
    {
        var permission = await GetOrNullAsync(name);
        if (permission == null)
        {
            throw new CrmException("Undefined permission: " + name);
        }

        return permission;
    }

    public virtual async Task<PermissionDefinition?> GetOrNullAsync(string name)
    {
        return await _staticStore.GetOrNullAsync(name) ??
               await _dynamicStore.GetOrNullAsync(name);
    }

    public virtual async Task<IReadOnlyList<PermissionDefinition>> GetPermissionsAsync()
    {
        var staticPermissions = await _staticStore.GetPermissionsAsync();
        var staticPermissionNames = staticPermissions
            .Select(p => p.Name)
            .ToImmutableHashSet();

        var dynamicPermissions = await _dynamicStore.GetPermissionsAsync();

        /* We prefer static permissions over dynamics */
        return staticPermissions.Concat(
            dynamicPermissions.Where(d => !staticPermissionNames.Contains(d.Name))
        ).ToImmutableList();
    }

    public async Task<IReadOnlyList<PermissionGroupDefinition>> GetGroupsAsync()
    {
        var staticGroups = await _staticStore.GetGroupsAsync();
        var staticGroupNames = staticGroups
            .Select(p => p.Name)
            .ToImmutableHashSet();

        var dynamicGroups = await _dynamicStore.GetGroupsAsync();

        /* We prefer static groups over dynamics */
        return staticGroups.Concat(
            dynamicGroups.Where(d => !staticGroupNames.Contains(d.Name))
        ).ToImmutableList();
    }
}