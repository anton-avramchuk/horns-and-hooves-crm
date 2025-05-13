using HornsAndHoovesCrm.Core.Modularity;
using HornsAndHoovesCrm.Modules.EntityFramework;
using HornsAndHoovesCrm.Modules.ReferenceData.Domain;
using HornsAndHoovesCrm.Modules.ReferenceData.Domain.Shared;

namespace HornsAndHoovesCrm.Modules.ReferenceData.DataAccess;

[DependsOn(typeof(EntityFrameworkModule), typeof(CrmReferenceDataDomainSharedModule),
    typeof(CrmReferenceDataDomainModule))]
public class CrmReferenceDataDataAccessModule : CrmModule
{
}