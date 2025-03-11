using System.Security.Claims;

namespace HornsAndHoovesCrm.Security.Claims.Abstractions;

public interface ICurrentPrincipalAccessor
{
    ClaimsPrincipal Principal { get; }

    IDisposable Change(ClaimsPrincipal principal);
}