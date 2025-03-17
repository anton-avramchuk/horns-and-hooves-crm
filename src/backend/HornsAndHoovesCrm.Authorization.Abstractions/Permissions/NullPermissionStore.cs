using HornsAndHoovesCrm.Core.DependencyInjection;

namespace HornsAndHoovesCrm.Authorization.Abstractions.Permissions;

public class NullPermissionStore : IPermissionStore, ISingletonDependency
{
    //todo: uncomment
    //public ILogger<NullPermissionStore> Logger { get; set; }

    public NullPermissionStore()
    {
        //Logger = NullLogger<NullPermissionStore>.Instance;
    }

    public Task<bool> IsGrantedAsync(string name, string providerName, string providerKey)
    {
        return Task.FromResult(false);
    }

    public Task<MultiplePermissionGrantResult> IsGrantedAsync(string[] names, string providerName, string providerKey)
    {
        return Task.FromResult(new MultiplePermissionGrantResult(names, PermissionGrantResult.Prohibited));
    }
}