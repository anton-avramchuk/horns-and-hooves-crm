using HornsAndHoovesCrm.Core.Modularity;
using HornsAndHoovesCrm.ReferenceData.Contracts;
using HornsAndHoovesCrm.ReferenceData.DataAccess;

namespace HornsAndHoovesCrm.ReferenceData.Api;

[DependsOn(typeof(CrmReferenceDataContractsModule), typeof(CrmReferenceDataDataAccessModule))]
public class CrmReferenceDataApiModule : CrmModule
{
}