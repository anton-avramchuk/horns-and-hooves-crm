using HornsAndHoovesCrm.Core.Modularity;
using HornsAndHoovesCrm.Domain;

namespace HornsAndHoovesCrm.ReferenceData.Domain;

[DependsOn(typeof(CrmDomainModule))]
public class CrmReferenceDataDomainModule : CrmModule
{
}