using HornsAndHoovesCrm.Core.DependencyInjection;

namespace HornsAndHoovesCrm.Core;

public class ApplicationInitializationContext : IServiceProviderAccessor
{
    public IServiceProvider ServiceProvider { get; set; }

    public ApplicationInitializationContext(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
    }
}