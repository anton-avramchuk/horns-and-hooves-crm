using HornsAndHoovesCrm.Core.Modularity;
using HornsAndHoovesCrm.Modules.ReferenceData.Contracts;
using HornsAndHoovesCrm.Modules.ReferenceData.DataAccess;

namespace HornsAndHoovesCrm.Modules.ReferenceData.Api;

[DependsOn(typeof(CrmReferenceDataContractsModule), typeof(CrmReferenceDataDataAccessModule))]
public class CrmReferenceDataApiModule : CrmModule
{
}