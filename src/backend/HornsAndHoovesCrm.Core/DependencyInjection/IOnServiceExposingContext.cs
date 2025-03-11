namespace HornsAndHoovesCrm.Core.DependencyInjection;

public interface IOnServiceExposingContext
{
    Type ImplementationType { get; }

    List<Type> ExposedTypes { get; }
}