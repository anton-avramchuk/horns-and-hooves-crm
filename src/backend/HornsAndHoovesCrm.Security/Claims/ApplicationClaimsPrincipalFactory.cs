using System.Security.Claims;
using HornsAndHoovesCrm.Core.DependencyInjection;
using HornsAndHoovesCrm.Security.Claims.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace HornsAndHoovesCrm.Security.Claims;

public class ApplicationClaimsPrincipalFactory : IApplicationClaimsPrincipalFactory, ITransientDependency
{
    public IServiceScopeFactory ServiceScopeFactory { get; }

    public ApplicationClaimsPrincipalFactory(IServiceScopeFactory serviceScopeFactory)
    {
        ServiceScopeFactory = serviceScopeFactory ?? throw new ArgumentNullException(nameof(serviceScopeFactory));
    }

    public Task<ClaimsPrincipal> CreateAsync(ClaimsPrincipal existsClaimsPrincipal = null)
    {
        using (var scope = ServiceScopeFactory.CreateScope())
        {
            var claimsPrincipal = existsClaimsPrincipal ?? new ClaimsPrincipal(new ClaimsIdentity(
                "Application",
                ApplicationClaimTypes.UserName,
                ApplicationClaimTypes.Role));

            var context = new ApplicationClaimsPrincipalContributorContext(claimsPrincipal, scope.ServiceProvider);


            return Task.FromResult(claimsPrincipal);
            //foreach (var contributorType in Options.Contributors)
            //{
            //    var contributor = (IAbpClaimsPrincipalContributor)scope.ServiceProvider.GetRequiredService(contributorType);
            //    await contributor.ContributeAsync(context);
            //}

            //return claimsPrincipal;
        }
    }
}