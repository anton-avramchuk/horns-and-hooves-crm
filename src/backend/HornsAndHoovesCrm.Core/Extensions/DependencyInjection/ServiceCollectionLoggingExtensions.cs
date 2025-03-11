using HornsAndHoovesCrm.Core.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace HornsAndHoovesCrm.Core.Extensions.DependencyInjection;

public static class ServiceCollectionLoggingExtensions
{
    public static ILogger<T> GetInitLogger<T>(this IServiceCollection services)
    {
        return services.GetSingletonInstance<IInitLoggerFactory>().Create<T>();
    }
}