namespace HornsAndHoovesCrm.Authorization.Abstractions.Permissions;

public interface IPermissionValueProviderManager
{
    IReadOnlyList<IPermissionValueProvider> ValueProviders { get; }
}