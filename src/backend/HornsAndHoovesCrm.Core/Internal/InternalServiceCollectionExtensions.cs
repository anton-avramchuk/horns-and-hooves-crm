using HornsAndHoovesCrm.Core.Configuration;
using HornsAndHoovesCrm.Core.Extensions.DependencyInjection;
using HornsAndHoovesCrm.Core.Logging;
using HornsAndHoovesCrm.Core.Modularity;
using HornsAndHoovesCrm.Core.Modularity.Abstractions;
using HornsAndHoovesCrm.Core.Reflection;
using HornsAndHoovesCrm.Core.SimpleStateChecking;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace HornsAndHoovesCrm.Core.Internal;

internal static class InternalServiceCollectionExtensions
{
    internal static void AddCoreServices(this IServiceCollection services)
    {
        services.AddOptions();
        services.AddLogging();
    }

    internal static void AddCoreCrmServices(this IServiceCollection services,
        ICrmApplication crmApplication,
        CrmApplicationCreationOptions applicationCreationOptions)
    {
        var moduleLoader = new ModuleLoader();
        var assemblyFinder = new AssemblyFinder(crmApplication);
        var typeFinder = new TypeFinder(assemblyFinder);

        if (!services.IsAdded<IConfiguration>())
        {
            services.ReplaceConfiguration(
                ConfigurationHelper.BuildConfiguration(
                    applicationCreationOptions.Configuration
                )
            );
        }

        services.TryAddSingleton<IModuleLoader>(moduleLoader);
        services.TryAddSingleton<IAssemblyFinder>(assemblyFinder);
        services.TryAddSingleton<ITypeFinder>(typeFinder);
        services.TryAddSingleton<IInitLoggerFactory>(new DefaultInitLoggerFactory());

        services.AddAssemblyOf<ICrmApplication>();

        services.AddTransient(typeof(ISimpleStateCheckerManager<>), typeof(SimpleStateCheckerManager<>));

        services.Configure<CrmModuleLifecycleOptions>(options =>
        {
            options.Contributors.Add<OnPreApplicationInitializationModuleLifecycleContributor>();
            options.Contributors.Add<OnApplicationInitializationModuleLifecycleContributor>();
            options.Contributors.Add<OnPostApplicationInitializationModuleLifecycleContributor>();
            options.Contributors.Add<OnApplicationShutdownModuleLifecycleContributor>();
        });
    }
}