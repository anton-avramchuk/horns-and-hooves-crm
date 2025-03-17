namespace HornsAndHoovesCrm.Authorization.Abstractions.Permissions;

public interface IPermissionDefinitionProvider
{
    void PreDefine(IPermissionDefinitionContext context);

    void Define(IPermissionDefinitionContext context);

    void PostDefine(IPermissionDefinitionContext context);
}