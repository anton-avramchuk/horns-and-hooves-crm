using HornsAndHoovesCrm.Core.Modularity;
using HornsAndHoovesCrm.Modules.ReferenceData.Admin.Contracts;
using HornsAndHoovesCrm.Modules.ReferenceData.DataAccess;

namespace HornsAndHoovesCrm.Modules.ReferenceData.Admin.Api;

[DependsOn(typeof(CrmReferenceDataAdminContractsModule), typeof(CrmReferenceDataDataAccessModule))]
public class CrmReferenceDataAdminApiModule : CrmModule
{
}