using HornsAndHoovesCrm.Core.DependencyInjection;

namespace HornsAndHoovesCrm.Core.ExceptionHandling;

[ExposeServices(typeof(IExceptionSubscriber))]
public abstract class ExceptionSubscriber : IExceptionSubscriber, ITransientDependency
{
    public abstract Task HandleAsync(ExceptionNotificationContext context);
}