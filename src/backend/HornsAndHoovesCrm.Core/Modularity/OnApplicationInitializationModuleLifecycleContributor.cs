using HornsAndHoovesCrm.Core.Modularity.Abstractions;

namespace HornsAndHoovesCrm.Core.Modularity;

public class OnApplicationInitializationModuleLifecycleContributor : ModuleLifecycleContributorBase
{
    public override async Task InitializeAsync(ApplicationInitializationContext context, ICrmModule module)
    {
        if (module is IOnApplicationInitialization onApplicationInitialization)
        {
            await onApplicationInitialization.OnApplicationInitializationAsync(context);
        }
    }

    public override void Initialize(ApplicationInitializationContext context, ICrmModule module)
    {
        (module as IOnApplicationInitialization)?.OnApplicationInitialization(context);
    }
}