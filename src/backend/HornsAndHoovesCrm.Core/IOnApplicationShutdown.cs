namespace HornsAndHoovesCrm.Core;

public interface IOnApplicationShutdown
{
    Task OnApplicationShutdownAsync(ApplicationShutdownContext context);

    void OnApplicationShutdown(ApplicationShutdownContext context);
}