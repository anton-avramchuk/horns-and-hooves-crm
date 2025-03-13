using HornsAndHoovesCrm.Domain;

namespace HornsAndHoovesCrm.Modules.Identity.Domain;

public class IdentityUserRole<TIdentityRole> : Entity where TIdentityRole : IdentityRole
{
    /// <summary>
    /// Gets or sets the primary key of the user that is linked to a role.
    /// </summary>
    public Guid UserId { get; private set; }


    /// <summary>
    /// Gets or sets the primary key of the role that is linked to the user.
    /// </summary>
    public Guid RoleId { get; private set; }

    public TIdentityRole Role { get;private set; }

    private IdentityUserRole()
    {

    }

    public void SetRole(TIdentityRole role)
    {
        Role = role;
        RoleId = role.Id;
    }

    public IdentityUserRole(Guid userId, TIdentityRole role)
    {
        UserId = userId;
        SetRole(role);
    }

}