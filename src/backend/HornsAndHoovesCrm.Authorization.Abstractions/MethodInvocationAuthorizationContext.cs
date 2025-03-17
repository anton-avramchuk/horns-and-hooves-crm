using System.Reflection;

namespace HornsAndHoovesCrm.Authorization.Abstractions;

public class MethodInvocationAuthorizationContext
{
    public MethodInfo Method { get; }

    public MethodInvocationAuthorizationContext(MethodInfo method)
    {
        Method = method;
    }
}