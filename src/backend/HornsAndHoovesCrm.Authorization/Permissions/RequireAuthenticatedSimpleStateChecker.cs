using HornsAndHoovesCrm.Core.SimpleStateChecking;
using HornsAndHoovesCrm.Security.Users.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace HornsAndHoovesCrm.Authorization.Permissions;

public class RequireAuthenticatedSimpleStateChecker<TState> : ISimpleStateChecker<TState>
    where TState : IHasSimpleStateCheckers<TState>
{
    public Task<bool> IsEnabledAsync(SimpleStateCheckerContext<TState> context)
    {
        return Task.FromResult(context.ServiceProvider.GetRequiredService<ICurrentUser>().IsAuthenticated);
    }
}