namespace HornsAndHoovesCrm.Core.Modularity.Abstractions;

public interface IPreConfigureServices
{
    Task PreConfigureServicesAsync(ServiceConfigurationContext context);

    void PreConfigureServices(ServiceConfigurationContext context);
}