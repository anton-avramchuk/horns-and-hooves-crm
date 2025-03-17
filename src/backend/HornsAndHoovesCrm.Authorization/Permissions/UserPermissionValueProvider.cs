using HornsAndHoovesCrm.Authorization.Abstractions.Permissions;
using HornsAndHoovesCrm.Security.Claims;

namespace HornsAndHoovesCrm.Authorization.Permissions;

public class UserPermissionValueProvider : PermissionValueProvider
{
    public const string ProviderName = "U";

    public override string Name => ProviderName;

    public UserPermissionValueProvider(IPermissionStore permissionStore)
        : base(permissionStore)
    {

    }

    public override async Task<PermissionGrantResult> CheckAsync(PermissionValueCheckContext context)
    {
        var userId = context.Principal?.FindFirst(ApplicationClaimTypes.UserId)?.Value;

        if (userId == null)
        {
            return PermissionGrantResult.Undefined;
        }

        return await PermissionStore.IsGrantedAsync(context.Permission.Name, Name, userId)
            ? PermissionGrantResult.Granted
            : PermissionGrantResult.Undefined;
    }

    public override async Task<MultiplePermissionGrantResult> CheckAsync(PermissionValuesCheckContext context)
    {
        var permissionNames = context.Permissions.Select(x => x.Name).Distinct().ToArray();

        var userId = context.Principal?.FindFirst(ApplicationClaimTypes.UserId)?.Value;
        if (userId == null)
        {
            return new MultiplePermissionGrantResult(permissionNames);
        }

        return await PermissionStore.IsGrantedAsync(permissionNames, Name, userId);
    }
}