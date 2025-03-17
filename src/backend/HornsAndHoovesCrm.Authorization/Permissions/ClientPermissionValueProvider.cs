using HornsAndHoovesCrm.Authorization.Abstractions.Permissions;
using HornsAndHoovesCrm.Security.Claims;

namespace HornsAndHoovesCrm.Authorization.Permissions;

public class ClientPermissionValueProvider : PermissionValueProvider
{
    public const string ProviderName = "C";

    public override string Name => ProviderName;


    public ClientPermissionValueProvider(IPermissionStore permissionStore)
        : base(permissionStore)
    {
    }

    public override async Task<PermissionGrantResult> CheckAsync(PermissionValueCheckContext context)
    {
        var clientId = context.Principal?.FindFirst(ApplicationClaimTypes.ClientId)?.Value;

        if (clientId == null)
        {
            return PermissionGrantResult.Undefined;
        }

        return await PermissionStore.IsGrantedAsync(context.Permission.Name, Name, clientId)
            ? PermissionGrantResult.Granted
            : PermissionGrantResult.Undefined;
    }

    public override async Task<MultiplePermissionGrantResult> CheckAsync(PermissionValuesCheckContext context)
    {
        var permissionNames = context.Permissions.Select(x => x.Name).Distinct().ToArray();

        var clientId = context.Principal?.FindFirst(ApplicationClaimTypes.ClientId)?.Value;
        if (clientId == null)
        {
            return new MultiplePermissionGrantResult(permissionNames); ;
        }
        return await PermissionStore.IsGrantedAsync(permissionNames, Name, clientId);
    }
}