using System.Collections.Immutable;
using HornsAndHoovesCrm.Core.Extensions.Collections;

namespace HornsAndHoovesCrm.Authorization.Abstractions.Permissions;

public class PermissionGroupDefinition : ICanAddChildPermission
{
    /// <summary>
    /// Unique name of the group.
    /// </summary>
    public string Name { get; }

    public Dictionary<string, object?> Properties { get; }

    public string DisplayName
    {
        get => _displayName;
        set => _displayName = value ?? throw new ArgumentNullException(nameof(value));
    }
    private string _displayName = default!;

    public IReadOnlyList<PermissionDefinition> Permissions => _permissions.ToImmutableList();
    private readonly List<PermissionDefinition> _permissions;

    /// <summary>
    /// Gets/sets a key-value on the <see cref="Properties"/>.
    /// </summary>
    /// <param name="name">Name of the property</param>
    /// <returns>
    /// Returns the value in the <see cref="Properties"/> dictionary by given <paramref name="name"/>.
    /// Returns null if given <paramref name="name"/> is not present in the <see cref="Properties"/> dictionary.
    /// </returns>
    public object? this[string name]
    {
        get => Properties.GetOrDefault(name);
        set => Properties[name] = value;
    }

    protected internal PermissionGroupDefinition(
        string name,
        string displayName)
    {
        Name = name;
        DisplayName = displayName;

        Properties = new Dictionary<string, object?>();
        _permissions = new List<PermissionDefinition>();
    }

    public virtual PermissionDefinition AddPermission(
        string name,
        string displayName,
        bool isEnabled = true)
    {
        var permission = new PermissionDefinition(
            name,
            displayName,
            isEnabled
        );

        _permissions.Add(permission);

        return permission;
    }

    public virtual List<PermissionDefinition> GetPermissionsWithChildren()
    {
        var permissions = new List<PermissionDefinition>();

        foreach (var permission in _permissions)
        {
            AddPermissionToListRecursively(permissions, permission);
        }

        return permissions;
    }

    private void AddPermissionToListRecursively(List<PermissionDefinition> permissions, PermissionDefinition permission)
    {
        permissions.Add(permission);

        foreach (var child in permission.Children)
        {
            AddPermissionToListRecursively(permissions, child);
        }
    }

    public override string ToString()
    {
        return $"[{nameof(PermissionGroupDefinition)} {Name}]";
    }

    public PermissionDefinition? GetPermissionOrNull(string name)
    {
        return GetPermissionOrNullRecursively(Permissions, name);
    }

    private PermissionDefinition? GetPermissionOrNullRecursively(
        IReadOnlyList<PermissionDefinition> permissions, string name)
    {
        foreach (var permission in permissions)
        {
            if (permission.Name == name)
            {
                return permission;
            }

            var childPermission = GetPermissionOrNullRecursively(permission.Children, name);
            if (childPermission != null)
            {
                return childPermission;
            }
        }

        return null;
    }
}