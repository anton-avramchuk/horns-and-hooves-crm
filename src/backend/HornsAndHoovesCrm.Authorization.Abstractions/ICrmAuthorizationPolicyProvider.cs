using Microsoft.AspNetCore.Authorization;

namespace HornsAndHoovesCrm.Authorization.Abstractions;

public interface ICrmAuthorizationPolicyProvider : IAuthorizationPolicyProvider
{
    Task<List<string>> GetPoliciesNamesAsync();
}