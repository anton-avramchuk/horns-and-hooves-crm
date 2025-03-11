namespace HornsAndHoovesCrm.Core.DynamicProxy;

public interface ICrmInterceptor
{
    Task InterceptAsync(ICrmMethodInvocation invocation);
}