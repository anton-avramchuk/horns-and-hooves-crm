using HornsAndHoovesCrm.Core.Modularity.Abstractions;

namespace HornsAndHoovesCrm.Core.Modularity;

public class OnPreApplicationInitializationModuleLifecycleContributor : ModuleLifecycleContributorBase
{
    public override async Task InitializeAsync(ApplicationInitializationContext context, ICrmModule module)
    {
        if (module is IOnPreApplicationInitialization onPreApplicationInitialization)
        {
            await onPreApplicationInitialization.OnPreApplicationInitializationAsync(context);
        }
    }

    public override void Initialize(ApplicationInitializationContext context, ICrmModule module)
    {
        (module as IOnPreApplicationInitialization)?.OnPreApplicationInitialization(context);
    }
}