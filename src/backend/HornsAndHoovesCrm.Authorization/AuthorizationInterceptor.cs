using HornsAndHoovesCrm.Authorization.Abstractions;
using HornsAndHoovesCrm.Core.DependencyInjection;
using HornsAndHoovesCrm.Core.DynamicProxy;

namespace HornsAndHoovesCrm.Authorization;

public class AuthorizationInterceptor : CrmInterceptor, ITransientDependency
{
    private readonly IMethodInvocationAuthorizationService _methodInvocationAuthorizationService;

    public AuthorizationInterceptor(IMethodInvocationAuthorizationService methodInvocationAuthorizationService)
    {
        _methodInvocationAuthorizationService = methodInvocationAuthorizationService;
    }

    public override async Task InterceptAsync(ICrmMethodInvocation invocation)
    {
        await AuthorizeAsync(invocation);
        await invocation.ProceedAsync();
    }

    protected virtual async Task AuthorizeAsync(ICrmMethodInvocation invocation)
    {
        await _methodInvocationAuthorizationService.CheckAsync(
            new MethodInvocationAuthorizationContext(
                invocation.Method
            )
        );
    }
}