using System.Security.Claims;

namespace HornsAndHoovesCrm.Modules.Identity.Domain;

public class IdentityRoleClaim : IdentityClaim
{
    public Guid RoleId { get; protected set; }


    protected IdentityRoleClaim()
    {

    }



    protected internal IdentityRoleClaim(
        Guid roleId,
        Claim claim)
        : base(claim)
    {
        RoleId = roleId;
    }

    public IdentityRoleClaim(
        Guid roleId,
        string claimType,
        string claimValue
    )
        : base(
            claimType,
            claimValue
        )
    {
        RoleId = roleId;
    }
}