using System.Security.Claims;

namespace HornsAndHoovesCrm.Security.Claims;

public class ApplicationClaimsPrincipalContributorContext
{
    public ClaimsPrincipal ClaimsIdentity { get; }
    public IServiceProvider ServiceProvider { get; }

    public ApplicationClaimsPrincipalContributorContext(
        ClaimsPrincipal claimsIdentity,
        IServiceProvider serviceProvider)
    {
        ClaimsIdentity = claimsIdentity ?? throw new ArgumentNullException(nameof(claimsIdentity));
        ServiceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
    }
}