using System.Security.Claims;

namespace HornsAndHoovesCrm.Modules.Identity.Domain;

public class IdentityUserClaim : IdentityClaim
{
    /// <summary>
    /// Gets or sets the primary key of the user associated with this claim.
    /// </summary>
    public Guid UserId { get; protected set; }

    protected IdentityUserClaim()
    {

    }

    protected internal IdentityUserClaim(Guid userId, Claim claim)
        : base(claim)
    {
        UserId = userId;
    }

    public IdentityUserClaim(Guid userId, string claimType, string claimValue)
        : base(claimType, claimValue)
    {
        UserId = userId;
    }
}