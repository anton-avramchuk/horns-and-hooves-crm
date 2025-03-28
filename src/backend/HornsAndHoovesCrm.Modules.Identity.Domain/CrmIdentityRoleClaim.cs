using System.Security.Claims;

namespace HornsAndHoovesCrm.Modules.Identity.Domain;

public class CrmIdentityRoleClaim : CrmIdentityClaim
{
    public Guid RoleId { get; protected set; }


    protected CrmIdentityRoleClaim()
    {

    }



    protected internal CrmIdentityRoleClaim(
        Guid roleId,
        Claim claim)
        : base(claim)
    {
        RoleId = roleId;
    }

    public CrmIdentityRoleClaim(
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