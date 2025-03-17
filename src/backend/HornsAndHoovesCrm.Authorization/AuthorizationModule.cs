using HornsAndHoovesCrm.Authorization.Abstractions;
using HornsAndHoovesCrm.Authorization.Abstractions.Permissions;
using HornsAndHoovesCrm.Authorization.Permissions;
using HornsAndHoovesCrm.Core.Extensions.Collections;
using HornsAndHoovesCrm.Core.Extensions.DependencyInjection;
using HornsAndHoovesCrm.Core.Modularity;
using HornsAndHoovesCrm.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace HornsAndHoovesCrm.Authorization;

[DependsOn(typeof(SecurityModule),typeof(AuthorizationAbstractionModule))]
public class AuthorizationModule : CrmModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.OnRegistered(AuthorizationInterceptorRegistrar.RegisterIfNeeded);
        AutoAddDefinitionProviders(context.Services);
    }


    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAuthorizationCore();

        context.Services.AddSingleton<IAuthorizationHandler, PermissionRequirementHandler>();
        context.Services.AddSingleton<IAuthorizationHandler, PermissionsRequirementHandler>();

        context.Services.TryAddTransient<DefaultAuthorizationPolicyProvider>();

        Configure<CrmPermissionOptions>(options =>
        {
            options.ValueProviders.Add<UserPermissionValueProvider>();
            options.ValueProviders.Add<RolePermissionValueProvider>();
            options.ValueProviders.Add<ClientPermissionValueProvider>();
        });
    }

    private static void AutoAddDefinitionProviders(IServiceCollection services)
    {
        var definitionProviders = new List<Type>();

        services.OnRegistered(context =>
        {
            if (typeof(IPermissionDefinitionProvider).IsAssignableFrom(context.ImplementationType))
            {
                definitionProviders.Add(context.ImplementationType);
            }
        });

        services.Configure<CrmPermissionOptions>(options =>
        {
            options.DefinitionProviders.AddIfNotContains(definitionProviders);
        });
    }
}