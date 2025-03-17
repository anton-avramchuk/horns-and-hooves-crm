using System.Security.Claims;
using HornsAndHoovesCrm.Authorization.Abstractions;
using HornsAndHoovesCrm.Core.DependencyInjection;
using HornsAndHoovesCrm.Security.Claims.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace HornsAndHoovesCrm.Authorization;

[Dependency(ReplaceServices = true)]
public class CrmAuthorizationService : DefaultAuthorizationService, ICrmAuthorizationService, ITransientDependency
{
    public IServiceProvider ServiceProvider { get; }

    public ClaimsPrincipal CurrentPrincipal => _currentPrincipalAccessor.Principal;

    private readonly ICurrentPrincipalAccessor _currentPrincipalAccessor;

    public CrmAuthorizationService(
        IAuthorizationPolicyProvider policyProvider,
        IAuthorizationHandlerProvider handlers,
        ILogger<DefaultAuthorizationService> logger,
        IAuthorizationHandlerContextFactory contextFactory,
        IAuthorizationEvaluator evaluator,
        IOptions<AuthorizationOptions> options,
        ICurrentPrincipalAccessor currentPrincipalAccessor,
        IServiceProvider serviceProvider)
        : base(
            policyProvider,
            handlers,
            logger,
            contextFactory,
            evaluator,
            options)
    {
        _currentPrincipalAccessor = currentPrincipalAccessor;
        ServiceProvider = serviceProvider;
    }
}