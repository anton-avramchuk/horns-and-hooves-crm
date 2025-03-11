using System.Reflection;

namespace HornsAndHoovesCrm.Core.Modularity.Abstractions;

public interface IAdditionalModuleAssemblyProvider
{
    Assembly[] GetAssemblies();
}