using HornsAndHoovesCrm.Core.Modularity;
using HornsAndHoovesCrm.Modules.EntityFramework;
using HornsAndHoovesCrm.ReferenceData.Domain;
using HornsAndHoovesCrm.ReferenceData.Domain.Shared;

namespace HornsAndHoovesCrm.ReferenceData.DataAccess;

[DependsOn(typeof(EntityFrameworkModule), typeof(CrmReferenceDataDomainSharedModule),
    typeof(CrmReferenceDataDomainModule))]
public class CrmReferenceDataDataAccessModule : CrmModule
{
}