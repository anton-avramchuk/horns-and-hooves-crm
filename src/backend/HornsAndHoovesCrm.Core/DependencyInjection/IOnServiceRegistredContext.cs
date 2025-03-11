using HornsAndHoovesCrm.Core.Colllections;
using HornsAndHoovesCrm.Core.DynamicProxy;

namespace HornsAndHoovesCrm.Core.DependencyInjection;

public interface IOnServiceRegistredContext
{
    ITypeList<ICrmInterceptor> Interceptors { get; }
    Type ImplementationType { get; }
}