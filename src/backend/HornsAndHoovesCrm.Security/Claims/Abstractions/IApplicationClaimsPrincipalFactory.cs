using System.Security.Claims;

namespace HornsAndHoovesCrm.Security.Claims.Abstractions;

public interface IApplicationClaimsPrincipalFactory
{
    Task<ClaimsPrincipal> CreateAsync(ClaimsPrincipal? existsClaimsPrincipal = null);
}