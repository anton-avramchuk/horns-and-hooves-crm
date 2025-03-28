using System.Security.Claims;

namespace HornsAndHoovesCrm.Modules.Identity.Domain;

public class CrmIdentityUserClaim : CrmIdentityClaim
{
    /// <summary>
    /// Gets or sets the primary key of the user associated with this claim.
    /// </summary>
    public Guid UserId { get; protected set; }

    protected CrmIdentityUserClaim()
    {

    }

    protected internal CrmIdentityUserClaim(Guid userId, Claim claim)
        : base(claim)
    {
        UserId = userId;
    }

    public CrmIdentityUserClaim(Guid userId, string claimType, string claimValue)
        : base(claimType, claimValue)
    {
        UserId = userId;
    }
}