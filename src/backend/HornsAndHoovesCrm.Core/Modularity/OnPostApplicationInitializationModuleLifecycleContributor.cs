using HornsAndHoovesCrm.Core.Modularity.Abstractions;

namespace HornsAndHoovesCrm.Core.Modularity;

public class OnPostApplicationInitializationModuleLifecycleContributor : ModuleLifecycleContributorBase
{
    public override async Task InitializeAsync(ApplicationInitializationContext context, ICrmModule module)
    {
        if (module is IOnPostApplicationInitialization onPostApplicationInitialization)
        {
            await onPostApplicationInitialization.OnPostApplicationInitializationAsync(context);
        }
    }

    public override void Initialize(ApplicationInitializationContext context, ICrmModule module)
    {
        (module as IOnPostApplicationInitialization)?.OnPostApplicationInitialization(context);
    }
}