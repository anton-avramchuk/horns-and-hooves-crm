using HornsAndHoovesCrm.Authorization.Abstractions;
using HornsAndHoovesCrm.Core.Exceptions;
using HornsAndHoovesCrm.Security.Exceptions;
using Microsoft.AspNetCore.Authorization;

namespace HornsAndHoovesCrm.Authorization.Extensions;

public static class CrmAuthorizationServiceExtensions
{
    public static async Task<AuthorizationResult> AuthorizeAsync(this IAuthorizationService authorizationService, string policyName)
    {
        return await authorizationService.AuthorizeAsync(
            null,
            policyName
        );
    }

    public static async Task<AuthorizationResult> AuthorizeAsync(this IAuthorizationService authorizationService, object resource, IAuthorizationRequirement requirement)
    {
        return await authorizationService.AuthorizeAsync(
            authorizationService.AsCrmAuthorizationService().CurrentPrincipal,
            resource,
            requirement
        );
    }

    public static async Task<AuthorizationResult> AuthorizeAsync(this IAuthorizationService authorizationService, object? resource, AuthorizationPolicy policy)
    {
        return await authorizationService.AuthorizeAsync(
            authorizationService.AsCrmAuthorizationService().CurrentPrincipal,
            resource,
            policy
        );
    }

    public static async Task<AuthorizationResult> AuthorizeAsync(this IAuthorizationService authorizationService, AuthorizationPolicy policy)
    {
        return await authorizationService.AuthorizeAsync(
            null,
            policy
        );
    }

    public static async Task<AuthorizationResult> AuthorizeAsync(this IAuthorizationService authorizationService, object resource, IEnumerable<IAuthorizationRequirement> requirements)
    {
        return await authorizationService.AuthorizeAsync(
            authorizationService.AsCrmAuthorizationService().CurrentPrincipal,
            resource,
            requirements
        );
    }

    public static async Task<AuthorizationResult> AuthorizeAsync(this IAuthorizationService authorizationService, object? resource, string policyName)
    {
        return await authorizationService.AuthorizeAsync(
            authorizationService.AsCrmAuthorizationService().CurrentPrincipal,
            resource,
            policyName
        );
    }

    public static async Task<bool> IsGrantedAsync(this IAuthorizationService authorizationService, string policyName)
    {
        return (await authorizationService.AuthorizeAsync(policyName)).Succeeded;
    }

    public static async Task<bool> IsGrantedAnyAsync(
        this IAuthorizationService authorizationService,
        params string[] policyNames)
    {

        foreach (var policyName in policyNames)
        {
            if ((await authorizationService.AuthorizeAsync(policyName)).Succeeded)
            {
                return true;
            }
        }

        return false;
    }

    public static async Task<bool> IsGrantedAsync(this IAuthorizationService authorizationService, object resource, IAuthorizationRequirement requirement)
    {
        return (await authorizationService.AuthorizeAsync(resource, requirement)).Succeeded;
    }

    public static async Task<bool> IsGrantedAsync(this IAuthorizationService authorizationService, object resource, AuthorizationPolicy policy)
    {
        return (await authorizationService.AuthorizeAsync(resource, policy)).Succeeded;
    }

    public static async Task<bool> IsGrantedAsync(this IAuthorizationService authorizationService, AuthorizationPolicy policy)
    {
        return (await authorizationService.AuthorizeAsync(policy)).Succeeded;
    }

    public static async Task<bool> IsGrantedAsync(this IAuthorizationService authorizationService, object resource, IEnumerable<IAuthorizationRequirement> requirements)
    {
        return (await authorizationService.AuthorizeAsync(resource, requirements)).Succeeded;
    }

    public static async Task<bool> IsGrantedAsync(this IAuthorizationService authorizationService, object resource, string policyName)
    {
        return (await authorizationService.AuthorizeAsync(resource, policyName)).Succeeded;
    }

    public static async Task CheckAsync(this IAuthorizationService authorizationService, string policyName)
    {
        if (!await authorizationService.IsGrantedAsync(policyName))
        {
            throw new CrmAuthorizationException(code: CrmAuthorizationErrorCodes.GivenPolicyHasNotGrantedWithPolicyName)
                .WithData("PolicyName", policyName);
        }
    }

    public static async Task CheckAsync(this IAuthorizationService authorizationService, object resource, IAuthorizationRequirement requirement)
    {
        if (!await authorizationService.IsGrantedAsync(resource, requirement))
        {
            throw new CrmAuthorizationException(code: CrmAuthorizationErrorCodes.GivenRequirementHasNotGrantedForGivenResource)
                .WithData("ResourceName", resource);
        }
    }

    public static async Task CheckAsync(this IAuthorizationService authorizationService, object resource, AuthorizationPolicy policy)
    {
        if (!await authorizationService.IsGrantedAsync(resource, policy))
        {
            throw new CrmAuthorizationException(code: CrmAuthorizationErrorCodes.GivenPolicyHasNotGrantedForGivenResource)
                .WithData("ResourceName", resource);
        }
    }

    public static async Task CheckAsync(this IAuthorizationService authorizationService, AuthorizationPolicy policy)
    {
        if (!await authorizationService.IsGrantedAsync(policy))
        {
            throw new CrmAuthorizationException(code: CrmAuthorizationErrorCodes.GivenPolicyHasNotGranted);
        }
    }

    public static async Task CheckAsync(this IAuthorizationService authorizationService, object resource, IEnumerable<IAuthorizationRequirement> requirements)
    {
        if (!await authorizationService.IsGrantedAsync(resource, requirements))
        {
            throw new CrmAuthorizationException(code: CrmAuthorizationErrorCodes.GivenRequirementsHasNotGrantedForGivenResource)
                .WithData("ResourceName", resource);
        }
    }

    public static async Task CheckAsync(this IAuthorizationService authorizationService, object resource, string policyName)
    {
        if (!await authorizationService.IsGrantedAsync(resource, policyName))
        {
            throw new CrmAuthorizationException(code: CrmAuthorizationErrorCodes.GivenPolicyHasNotGrantedForGivenResource)
                .WithData("ResourceName", resource);
        }
    }

    private static ICrmAuthorizationService AsCrmAuthorizationService(this IAuthorizationService authorizationService)
    {
        if (!(authorizationService is ICrmAuthorizationService crmAuthorizationService))
        {
            throw new CrmException($"{nameof(authorizationService)} should implement {typeof(ICrmAuthorizationService).FullName}");
        }

        return crmAuthorizationService;
    }
}