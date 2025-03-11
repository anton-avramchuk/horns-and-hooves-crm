using System.Security.Claims;
using HornsAndHoovesCrm.Core.DependencyInjection;

namespace HornsAndHoovesCrm.Security.Claims;

public class ThreadCurrentPrincipalAccessor : CurrentPrincipalAccessorBase, ISingletonDependency
{
    protected override ClaimsPrincipal GetClaimsPrincipal()
    {
        return Thread.CurrentPrincipal as ClaimsPrincipal;
    }
}