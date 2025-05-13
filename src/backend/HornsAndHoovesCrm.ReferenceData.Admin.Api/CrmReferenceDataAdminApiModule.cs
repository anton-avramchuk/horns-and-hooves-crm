using HornsAndHoovesCrm.Core.Modularity;
using HornsAndHoovesCrm.ReferenceData.Admin.Contracts;
using HornsAndHoovesCrm.ReferenceData.DataAccess;

namespace HornsAndHoovesCrm.ReferenceData.Admin.Api;

[DependsOn(typeof(CrmReferenceDataAdminContractsModule), typeof(CrmReferenceDataDataAccessModule))]
public class CrmReferenceDataAdminApiModule : CrmModule
{
}