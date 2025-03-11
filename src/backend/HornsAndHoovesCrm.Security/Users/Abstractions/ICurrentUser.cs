using System.Security.Claims;

namespace HornsAndHoovesCrm.Security.Users.Abstractions;

public interface ICurrentUser
{
    bool IsAuthenticated { get; }
    Guid? Id { get; }

    string UserName { get; }

    string Name { get; }

    string SurName { get; }

    string Email { get; }



    string[] Roles { get; }

    Claim FindClaim(string claimType);

    Claim[] FindClaims(string claimType);

    Claim[] GetAllClaims();

    bool IsInRole(string roleName);
}