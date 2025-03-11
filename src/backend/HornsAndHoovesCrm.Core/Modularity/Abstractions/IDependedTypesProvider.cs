namespace HornsAndHoovesCrm.Core.Modularity.Abstractions;

public interface IDependedTypesProvider
{

    Type[] GetDependedTypes();
}