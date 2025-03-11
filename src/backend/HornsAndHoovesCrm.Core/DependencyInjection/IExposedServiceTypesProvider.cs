namespace HornsAndHoovesCrm.Core.DependencyInjection;

public interface IExposedServiceTypesProvider
{
    Type[] GetExposedServiceTypes(Type targetType);
}