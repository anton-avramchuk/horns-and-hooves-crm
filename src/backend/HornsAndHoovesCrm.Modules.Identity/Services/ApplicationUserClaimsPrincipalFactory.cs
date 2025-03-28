using HornsAndHoovesCrm.Core.DependencyInjection;
using HornsAndHoovesCrm.Modules.Identity.Domain;
using HornsAndHoovesCrm.Security.Claims.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace HornsAndHoovesCrm.Modules.Identity.Services;

public class ApplicationUserClaimsPrincipalFactory<TIdentityUser, TIdentityRole> : UserClaimsPrincipalFactory<TIdentityUser, TIdentityRole>, ITransientDependency
    where TIdentityRole : CrmIdentityRole
    where TIdentityUser : CrmIdentityUser<TIdentityRole>
{
    public ICurrentPrincipalAccessor CurrentPrincipalAccessor { get; }

    public ApplicationUserClaimsPrincipalFactory(
        UserManager<TIdentityUser> userManager,
        RoleManager<TIdentityRole> roleManager,
        IOptions<IdentityOptions> options,
        ICurrentPrincipalAccessor currentPrincipalAccessor
    ) : base(userManager, roleManager, options)
    {
        CurrentPrincipalAccessor = currentPrincipalAccessor;
    }
}