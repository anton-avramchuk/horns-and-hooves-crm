using HornsAndHoovesCrm.Authorization.Abstractions.Permissions;
using HornsAndHoovesCrm.Core.DependencyInjection;
using HornsAndHoovesCrm.Core.Exceptions;
using HornsAndHoovesCrm.Core.Extensions.Collections;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace HornsAndHoovesCrm.Authorization.Permissions;

public class PermissionValueProviderManager : IPermissionValueProviderManager, ISingletonDependency
{
    public IReadOnlyList<IPermissionValueProvider> ValueProviders => _lazyProviders.Value;
    private readonly Lazy<List<IPermissionValueProvider>> _lazyProviders;

    protected CrmPermissionOptions Options { get; }
    protected IServiceProvider ServiceProvider { get; }

    public PermissionValueProviderManager(
        IServiceProvider serviceProvider,
        IOptions<CrmPermissionOptions> options)
    {
        Options = options.Value;
        ServiceProvider = serviceProvider;

        _lazyProviders = new Lazy<List<IPermissionValueProvider>>(GetProviders, true);
    }

    protected virtual List<IPermissionValueProvider> GetProviders()
    {
        var providers = Options
            .ValueProviders
            .Select(type => (ServiceProvider.GetRequiredService(type) as IPermissionValueProvider)!)
            .ToList();

        var multipleProviders = providers.GroupBy(p => p.Name).FirstOrDefault(x => x.Count() > 1);
        if (multipleProviders != null)
        {
            throw new CrmException($"Duplicate permission value provider name detected: {multipleProviders.Key}. Providers:{Environment.NewLine}{multipleProviders.Select(p => p.GetType().FullName!).JoinAsString(Environment.NewLine)}");
        }

        return providers;
    }
}