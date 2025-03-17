using HornsAndHoovesCrm.Core.DependencyInjection;

namespace HornsAndHoovesCrm.Authorization.Abstractions.Permissions;

public abstract class PermissionValueProvider : IPermissionValueProvider, ITransientDependency
{
    public abstract string Name { get; }

    protected IPermissionStore PermissionStore { get; }

    protected PermissionValueProvider(IPermissionStore permissionStore)
    {
        PermissionStore = permissionStore;
    }

    public abstract Task<PermissionGrantResult> CheckAsync(PermissionValueCheckContext context);

    public abstract Task<MultiplePermissionGrantResult> CheckAsync(PermissionValuesCheckContext context);
}