using HornsAndHoovesCrm.Core.Exceptions;

namespace HornsAndHoovesCrm.Authorization.Abstractions.Permissions;

public class PermissionDefinitionContext : IPermissionDefinitionContext
{
    public IServiceProvider ServiceProvider { get; }

    public Dictionary<string, PermissionGroupDefinition> Groups { get; }

    public PermissionDefinitionContext(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
        Groups = new Dictionary<string, PermissionGroupDefinition>();
    }

    public virtual PermissionGroupDefinition AddGroup(
        string name,
        string displayName)
    {
        

        if (Groups.ContainsKey(name))
        {
            throw new CrmException($"There is already an existing permission group with name: {name}");
        }

        return Groups[name] = new PermissionGroupDefinition(name, displayName);
    }

    
    public virtual PermissionGroupDefinition GetGroup(string name)
    {
        var group = GetGroupOrNull(name);

        if (group == null)
        {
            throw new CrmException($"Could not find a permission definition group with the given name: {name}");
        }

        return group;
    }

    public virtual PermissionGroupDefinition? GetGroupOrNull(string name)
    {

        if (!Groups.ContainsKey(name))
        {
            return null;
        }

        return Groups[name];
    }

    public virtual void RemoveGroup(string name)
    {

        if (!Groups.ContainsKey(name))
        {
            throw new CrmException($"Not found permission group with name: {name}");
        }

        Groups.Remove(name);
    }

    public virtual PermissionDefinition? GetPermissionOrNull(string name)
    {

        foreach (var groupDefinition in Groups.Values)
        {
            var permissionDefinition = groupDefinition.GetPermissionOrNull(name);

            if (permissionDefinition != null)
            {
                return permissionDefinition;
            }
        }

        return null;
    }
}