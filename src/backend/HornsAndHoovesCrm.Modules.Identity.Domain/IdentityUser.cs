using System.Security.Claims;
using HornsAndHoovesCrm.Domain;

namespace HornsAndHoovesCrm.Modules.Identity.Domain;

public class IdentityUser<TIdentityRole> : Entity<Guid> where TIdentityRole : IdentityRole
{
    public string UserName { get; protected internal set; }

    public string NormalizedUserName { get; protected internal set; }

    public string Email { get; protected internal set; }

    public virtual bool EmailConfirmed { get; protected internal set; }

    public string NormalizedEmail { get; protected internal set; }

    public string PasswordHash { get; protected internal set; }

    public string SecurityStamp { get; protected internal set; }

    public DateTimeOffset? LockoutEnd { get; protected internal set; }

    public bool LockoutEnabled { get; protected internal set; }

    public int AccessFailedCount { get; protected internal set; }

    private readonly List<IdentityUserRole<TIdentityRole>> _roles = new List<IdentityUserRole<TIdentityRole>>();
    public IReadOnlyCollection<IdentityUserRole<TIdentityRole>> Roles => _roles;

    private readonly List<IdentityUserClaim> _claims = new List<IdentityUserClaim>();

    public IReadOnlyCollection<IdentityUserClaim> Claims => _claims;

    public void ChangePasswordHash(string passwordHash)
    {
        PasswordHash = passwordHash;
    }

    public void ChangeEmailConfirmed(bool emailConfirmed)
    {
        EmailConfirmed = emailConfirmed;
    }


    public void ChangeSecurityStamp(string value)
    {
        SecurityStamp = value;
    }

    protected IdentityUser()
    {
    }

    public IdentityUser(
        string userName,
        string email
    )
        : base(Guid.NewGuid())
    {

        ChangeUserName(userName);
        ChangeNormalizedUserName(userName);
        ChangeEmail(email);
        ChangeNormalizedEmail(email);
        SecurityStamp = Guid.NewGuid().ToString();
    }


    public void ChangeUserName(string userName)
    {
        UserName = userName ?? throw new ArgumentNullException(nameof(userName));
        ChangeNormalizedUserName(userName);
    }


    public void ChangeEmail(string email)
    {
        Email = email ?? throw new ArgumentNullException(nameof(email));
        ChangeNormalizedEmail(email);
    }


    public void ChangeNormalizedEmail(string email)
    {
        if (email == null) throw new ArgumentNullException(nameof(email));
        NormalizedEmail = email.ToUpperInvariant();
    }

    public void ChangeNormalizedUserName(string userName)
    {
        if (userName == null) throw new ArgumentNullException(nameof(userName));
        NormalizedUserName = userName.ToUpperInvariant();
    }

    public virtual void AddRole(TIdentityRole role)
    {
        if (IsInRole(role.Id))
        {
            return;
        }

        _roles.Add(new IdentityUserRole<TIdentityRole>(Id, role));
    }

    public virtual void RemoveRole(TIdentityRole role)
    {
        if (!IsInRole(role))
        {
            return;
        }

        _roles.RemoveAll(r => r.RoleId == role.Id);
    }

    public virtual bool IsInRole(Guid roleId)
    {
        return Roles.Any(r => r.RoleId == roleId);
    }

    public virtual bool IsInRole(TIdentityRole role)
    {
        return Roles.Any(r => r.RoleId == role.Id);
    }

    public virtual void AddClaim(Claim claim)
    {
        if (claim == null) throw new ArgumentNullException(nameof(claim));
        _claims.Add(new IdentityUserClaim(Id, claim));
    }

    public virtual void AddClaims(IEnumerable<Claim> claims)
    {
        if (claims == null) throw new ArgumentNullException(nameof(claims));
        foreach (var claim in claims)
        {
            AddClaim(claim);
        }
    }

    public virtual IdentityUserClaim FindClaim(Claim claim)
    {
        if (claim == null) throw new ArgumentNullException(nameof(claim));
        return Claims.FirstOrDefault(c => c.ClaimType == claim.Type && c.ClaimValue == claim.Value);
    }

    public virtual void ReplaceClaim(Claim claim, Claim newClaim)
    {
        if (claim == null) throw new ArgumentNullException(nameof(claim));
        if (newClaim == null) throw new ArgumentNullException(nameof(newClaim));
        var userClaims = Claims.Where(uc => uc.ClaimValue == claim.Value && uc.ClaimType == claim.Type);
        foreach (var userClaim in userClaims)
        {
            userClaim.SetClaim(newClaim);
        }
    }

    public virtual void RemoveClaims(IEnumerable<Claim> claims)
    {
        if (claims == null) throw new ArgumentNullException(nameof(claims));
        foreach (var claim in claims)
        {
            RemoveClaim(claim);
        }
    }

    public virtual void RemoveClaim(Claim claim)
    {
        if (claim == null) throw new ArgumentNullException(nameof(claim));

        _claims.RemoveAll(c => c.ClaimValue == claim.Value && c.ClaimType == claim.Type);
    }



    public override string ToString()
    {
        return $"{base.ToString()}, UserName = {UserName}";
    }
}