using System.Security.Claims;
using HornsAndHoovesCrm.Core.DependencyInjection;
using HornsAndHoovesCrm.Security.Claims;
using HornsAndHoovesCrm.Security.Claims.Abstractions;
using HornsAndHoovesCrm.Security.Extensions;
using HornsAndHoovesCrm.Security.Users.Abstractions;

namespace HornsAndHoovesCrm.Security.Users;

public class CurrentUser : ICurrentUser, ITransientDependency
{
    private static readonly Claim[] EmptyClaimsArray = [];

    public virtual bool IsAuthenticated => Id.HasValue;

    public virtual Guid? Id => _principalAccessor.Principal?.FindUserId();

    public virtual string UserName => this.FindClaimValue(ApplicationClaimTypes.UserName);

    public virtual string Name => this.FindClaimValue(ApplicationClaimTypes.Name);

    public virtual string SurName => this.FindClaimValue(ApplicationClaimTypes.SurName);



    public virtual string Email => this.FindClaimValue(ApplicationClaimTypes.Email);



    public virtual string[] Roles => FindClaims(ApplicationClaimTypes.Role).Select(c => c.Value).Distinct().ToArray();

    private readonly ICurrentPrincipalAccessor _principalAccessor;

    public CurrentUser(ICurrentPrincipalAccessor principalAccessor)
    {
        _principalAccessor = principalAccessor;
    }

    public virtual Claim FindClaim(string claimType)
    {
        return _principalAccessor.Principal?.Claims.FirstOrDefault(c => c.Type == claimType);
    }

    public virtual Claim[] FindClaims(string claimType)
    {
        return _principalAccessor.Principal?.Claims.Where(c => c.Type == claimType).ToArray() ?? EmptyClaimsArray;
    }

    public virtual Claim[] GetAllClaims()
    {
        return _principalAccessor.Principal?.Claims.ToArray() ?? EmptyClaimsArray;
    }

    public virtual bool IsInRole(string roleName)
    {
        return FindClaims(ApplicationClaimTypes.Role).Any(c => c.Value == roleName);
    }
}