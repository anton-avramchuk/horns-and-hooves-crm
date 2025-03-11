using HornsAndHoovesCrm.Core.Colllections;
using HornsAndHoovesCrm.Core.Modularity.Abstractions;

namespace HornsAndHoovesCrm.Core.Modularity;

public class CrmModuleLifecycleOptions
{
    public ITypeList<IModuleLifecycleContributor> Contributors { get; }

    public CrmModuleLifecycleOptions()
    {
        Contributors = new TypeList<IModuleLifecycleContributor>();
    }
}