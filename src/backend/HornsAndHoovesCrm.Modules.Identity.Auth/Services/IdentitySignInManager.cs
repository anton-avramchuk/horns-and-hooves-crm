using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace HornsAndHoovesCrm.Modules.Identity.Auth.Services;

public class IdentitySignInManager<TIdentityUser, TIdentityRole> : SignInManager<TIdentityUser>
    where TIdentityUser : Domain.CrmIdentityUser<TIdentityRole>
    where TIdentityRole : Domain.CrmIdentityRole
{
    public IdentitySignInManager(UserManager<TIdentityUser> userManager, IHttpContextAccessor contextAccessor, IUserClaimsPrincipalFactory<TIdentityUser> claimsFactory, IOptions<IdentityOptions> optionsAccessor, ILogger<SignInManager<TIdentityUser>> logger, IAuthenticationSchemeProvider schemes, IUserConfirmation<TIdentityUser> confirmation) : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes, confirmation)
    {
            
    }
}