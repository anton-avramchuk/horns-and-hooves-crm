using System.Security.Claims;
using HornsAndHoovesCrm.Security.Claims.Abstractions;
using Microsoft.AspNetCore.Authorization;

namespace HornsAndHoovesCrm.Authorization.Abstractions;

public class AlwaysAllowAuthorizationService : ICrmAuthorizationService
{
    public IServiceProvider ServiceProvider { get; }

    public ClaimsPrincipal CurrentPrincipal => _currentPrincipalAccessor.Principal;

    private readonly ICurrentPrincipalAccessor _currentPrincipalAccessor;

    public AlwaysAllowAuthorizationService(IServiceProvider serviceProvider, ICurrentPrincipalAccessor currentPrincipalAccessor)
    {
        ServiceProvider = serviceProvider;
        _currentPrincipalAccessor = currentPrincipalAccessor;
    }

    public Task<AuthorizationResult> AuthorizeAsync(ClaimsPrincipal user, object? resource, IEnumerable<IAuthorizationRequirement> requirements)
    {
        return Task.FromResult(AuthorizationResult.Success());
    }

    public Task<AuthorizationResult> AuthorizeAsync(ClaimsPrincipal user, object? resource, string policyName)
    {
        return Task.FromResult(AuthorizationResult.Success());
    }
}