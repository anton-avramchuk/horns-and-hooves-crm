namespace HornsAndHoovesCrm.Core.DynamicProxy;

public abstract class CrmInterceptor : ICrmInterceptor
{
    public abstract Task InterceptAsync(ICrmMethodInvocation invocation);
}