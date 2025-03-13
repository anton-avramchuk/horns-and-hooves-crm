using System.Security.Claims;
using HornsAndHoovesCrm.Domain;

namespace HornsAndHoovesCrm.Modules.Identity.Domain;

public class IdentityRole : Entity<Guid>, IAggregateRoot
{
    public virtual string Name { get; protected internal set; }

    public virtual string NormalizedName { get; protected internal set; }


    private readonly List<IdentityRoleClaim> _claims = new List<IdentityRoleClaim>();

    public virtual IReadOnlyCollection<IdentityRoleClaim> Claims => _claims;

    protected IdentityRole()
    {

    }

    protected IdentityRole(Guid id) : base(id)
    {

    }


    public void ChangeName(string name)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        ChangeNormalizedName(name);
    }

    public void ChangeNormalizedName(string value)
    {
        if (value == null) throw new ArgumentNullException(nameof(value));

        NormalizedName = value.ToUpperInvariant();
    }


    public IdentityRole(string name) : base(Guid.NewGuid())
    {
        ChangeName(name);
    }


    public IdentityRole(Guid id, string name) : base(id)
    {
        ChangeName(name);
    }
        



    public virtual void AddClaim(Claim claim)
    {
        _claims.Add(new IdentityRoleClaim(Id, claim));
    }

    public virtual void RemoveClaim(Claim claim)
    {
        _claims.RemoveAll(c => c.ClaimType == claim.Type && c.ClaimValue == claim.Value);
    }


    public override string ToString()
    {
        return $"{base.ToString()}, Name = {Name}";
    }
}