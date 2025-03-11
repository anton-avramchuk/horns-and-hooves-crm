using Microsoft.Extensions.DependencyInjection;

namespace HornsAndHoovesCrm.Core.Modularity.Abstractions;

public interface IModuleLoader
{

    ICrmModuleDescriptor[] LoadModules(
        IServiceCollection services,
        Type startupModuleType
    );
}