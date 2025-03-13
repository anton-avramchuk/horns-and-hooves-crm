using System.Security.Claims;
using HornsAndHoovesCrm.Domain;

namespace HornsAndHoovesCrm.Modules.Identity.Domain;

public abstract class IdentityClaim : Entity<Guid>
{
    /// <summary>
    /// Gets or sets the claim type for this claim.
    /// </summary>
    public string ClaimType { get; protected set; }

    /// <summary>
    /// Gets or sets the claim value for this claim.
    /// </summary>
    public string ClaimValue { get; protected set; }

    protected IdentityClaim()
    {

    }

    protected IdentityClaim(Claim claim) : this(claim.Type, claim.Value)
    {

    }

    protected IdentityClaim(string claimType, string claimValue)
    {
        Id = Guid.NewGuid();
        ClaimType = claimType ?? throw new ArgumentNullException(nameof(claimType));
        ClaimValue = claimValue;
    }

    public virtual Claim ToClaim()
    {
        return new Claim(ClaimType, ClaimValue);
    }


    public virtual void SetClaim(Claim claim)
    {
        if (claim == null) throw new ArgumentNullException(nameof(claim));

        ClaimType = claim.Type;
        ClaimValue = claim.Value;
    }
}