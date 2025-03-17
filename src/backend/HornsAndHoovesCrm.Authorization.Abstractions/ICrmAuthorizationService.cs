using System.Security.Claims;
using HornsAndHoovesCrm.Core.DependencyInjection;
using Microsoft.AspNetCore.Authorization;

namespace HornsAndHoovesCrm.Authorization.Abstractions;

public interface ICrmAuthorizationService : IAuthorizationService, IServiceProviderAccessor
{
    ClaimsPrincipal CurrentPrincipal { get; }
}