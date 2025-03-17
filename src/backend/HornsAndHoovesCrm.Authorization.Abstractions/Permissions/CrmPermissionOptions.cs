using HornsAndHoovesCrm.Core.Colllections;
using HornsAndHoovesCrm.Core.Exceptions;

namespace HornsAndHoovesCrm.Authorization.Abstractions.Permissions;

public class CrmPermissionOptions
{
    public ITypeList<IPermissionDefinitionProvider> DefinitionProviders { get; }

    public ITypeList<IPermissionValueProvider> ValueProviders { get; }

    public HashSet<string> DeletedPermissions { get; }

    public HashSet<string> DeletedPermissionGroups { get; }

    public CrmPermissionOptions()
    {
        DefinitionProviders = new TypeList<IPermissionDefinitionProvider>();
        ValueProviders = new TypeList<IPermissionValueProvider>();

        DeletedPermissions = new HashSet<string>();
        DeletedPermissionGroups = new HashSet<string>();
    }
}

public interface ICanAddChildPermission
{
    PermissionDefinition AddPermission(
        string name,
        string displayName,
        bool isEnabled = true);
}

public interface IPermissionDefinitionContext
{
    //TODO: Add Get methods to find and modify a permission or group.

    IServiceProvider ServiceProvider { get; }

    /// <summary>
    /// Gets a pre-defined permission group.
    /// Throws <see cref="CrmException"/> if can not find the given group.
    /// </summary>
    /// <param name="name">Name of the group</param>
    /// <returns></returns>
    PermissionGroupDefinition GetGroup(string name);

    /// <summary>
    /// Tries to get a pre-defined permission group.
    /// Returns null if can not find the given group.
    /// </summary>
    /// <param name="name">Name of the group</param>
    /// <returns></returns>
    PermissionGroupDefinition? GetGroupOrNull(string name);

    /// <summary>
    /// Tries to add a new permission group.
    /// Throws <see cref="CrmException"/> if there is a group with the name.
    /// <param name="name">Name of the group</param>
    /// <param name="displayName">Localized display name of the group</param>
    /// <param name="multiTenancySide">Select a multi-tenancy side</param>
    /// </summary>
    PermissionGroupDefinition AddGroup(
        string name,
        string displayName);

    /// <summary>
    /// Tries to remove a permission group.
    /// Throws <see cref="CrmException"/> if there is not any group with the name.
    /// <param name="name">Name of the group</param>
    /// </summary>
    void RemoveGroup(string name);

    /// <summary>
    /// Tries to get a pre-defined permission group.
    /// Returns null if can not find the given group.
    /// <param name="name">Name of the group</param>
    /// </summary>
    PermissionDefinition? GetPermissionOrNull(string name);
}