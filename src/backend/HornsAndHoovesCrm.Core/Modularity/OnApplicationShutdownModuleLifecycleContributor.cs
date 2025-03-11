using HornsAndHoovesCrm.Core.Modularity.Abstractions;

namespace HornsAndHoovesCrm.Core.Modularity;

public class OnApplicationShutdownModuleLifecycleContributor : ModuleLifecycleContributorBase
{
    public override async Task ShutdownAsync(ApplicationShutdownContext context, ICrmModule module)
    {
        if (module is IOnApplicationShutdown onApplicationShutdown)
        {
            await onApplicationShutdown.OnApplicationShutdownAsync(context);
        }
    }

    public override void Shutdown(ApplicationShutdownContext context, ICrmModule module)
    {
        (module as IOnApplicationShutdown)?.OnApplicationShutdown(context);
    }
}