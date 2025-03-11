namespace HornsAndHoovesCrm.Core.Modularity.Abstractions;

public interface IModuleManager
{
    Task InitializeModulesAsync(ApplicationInitializationContext context);

    void InitializeModules(ApplicationInitializationContext context);

    Task ShutdownModulesAsync(ApplicationShutdownContext context);

    void ShutdownModules(ApplicationShutdownContext context);
}