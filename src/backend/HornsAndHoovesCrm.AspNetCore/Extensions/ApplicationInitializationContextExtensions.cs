using HornsAndHoovesCrm.Core;
using HornsAndHoovesCrm.Core.DependencyInjection;

namespace HornsAndHoovesCrm.AspNetCore.Extensions;

public static class ApplicationInitializationContextExtensions
{
    public static IApplicationBuilder GetApplicationBuilder(this ApplicationInitializationContext context)
    {
        return context.ServiceProvider.GetRequiredService<IObjectAccessor<IApplicationBuilder>>().Value!;
    }


    public static IEndpointRouteBuilder GetRouteBuilder(this ApplicationInitializationContext context)
    {
        return context.ServiceProvider.GetRequiredService<IObjectAccessor<IEndpointRouteBuilder>>().Value!;
    }


    public static IWebHostEnvironment GetEnvironment(this ApplicationInitializationContext context)
    {
        return context.ServiceProvider.GetRequiredService<IWebHostEnvironment>();
    }

    public static IWebHostEnvironment GetEnvironmentOrNull(this ApplicationInitializationContext context)
    {
        return context.ServiceProvider.GetService<IWebHostEnvironment>()!;
    }

    public static IConfiguration GetConfiguration(this ApplicationInitializationContext context)
    {
        return context.ServiceProvider.GetRequiredService<IConfiguration>();
    }

    public static ILoggerFactory GetLoggerFactory(this ApplicationInitializationContext context)
    {
        return context.ServiceProvider.GetRequiredService<ILoggerFactory>();
    }
}