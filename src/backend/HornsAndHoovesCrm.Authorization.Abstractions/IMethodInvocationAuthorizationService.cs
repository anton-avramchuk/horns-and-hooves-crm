namespace HornsAndHoovesCrm.Authorization.Abstractions;

public interface IMethodInvocationAuthorizationService
{
    Task CheckAsync(MethodInvocationAuthorizationContext context);
}