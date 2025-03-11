namespace HornsAndHoovesCrm.Core.Modularity.Abstractions;

public interface IOnPreApplicationInitialization
{
    Task OnPreApplicationInitializationAsync(ApplicationInitializationContext context);

    void OnPreApplicationInitialization(ApplicationInitializationContext context);
}