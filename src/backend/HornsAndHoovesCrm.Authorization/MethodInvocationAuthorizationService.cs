using System.Reflection;
using HornsAndHoovesCrm.Authorization.Abstractions;
using HornsAndHoovesCrm.Authorization.Extensions;
using HornsAndHoovesCrm.Core.DependencyInjection;
using Microsoft.AspNetCore.Authorization;

namespace HornsAndHoovesCrm.Authorization;

public class MethodInvocationAuthorizationService : IMethodInvocationAuthorizationService, ITransientDependency
{
    private readonly ICrmAuthorizationPolicyProvider _crmAuthorizationPolicyProvider;
    private readonly ICrmAuthorizationService _crmAuthorizationService;

    public MethodInvocationAuthorizationService(
        ICrmAuthorizationPolicyProvider crmAuthorizationPolicyProvider,
        ICrmAuthorizationService crmAuthorizationService)
    {
        _crmAuthorizationPolicyProvider = crmAuthorizationPolicyProvider;
        _crmAuthorizationService = crmAuthorizationService;
    }

    public async Task CheckAsync(MethodInvocationAuthorizationContext context)
    {
        if (AllowAnonymous(context))
        {
            return;
        }

        var authorizationPolicy = await AuthorizationPolicy.CombineAsync(
            _crmAuthorizationPolicyProvider,
            GetAuthorizationDataAttributes(context.Method)
        );

        if (authorizationPolicy == null)
        {
            return;
        }

        await _crmAuthorizationService.CheckAsync(authorizationPolicy);
    }

    protected virtual bool AllowAnonymous(MethodInvocationAuthorizationContext context)
    {
        return context.Method.GetCustomAttributes(true).OfType<IAllowAnonymous>().Any();
    }

    protected virtual IEnumerable<IAuthorizeData> GetAuthorizationDataAttributes(MethodInfo methodInfo)
    {
        var attributes = methodInfo
            .GetCustomAttributes(true)
            .OfType<IAuthorizeData>();

        if (methodInfo.IsPublic && methodInfo.DeclaringType != null)
        {
            attributes = attributes
                .Union(
                    methodInfo.DeclaringType
                        .GetCustomAttributes(true)
                        .OfType<IAuthorizeData>()
                );
        }

        return attributes;
    }
}