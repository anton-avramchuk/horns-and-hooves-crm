namespace HornsAndHoovesCrm.Authorization;

public static class CrmAuthorizationErrorCodes
{
    public const string GivenPolicyHasNotGranted = "Crm.Authorization:010001";

    public const string GivenPolicyHasNotGrantedWithPolicyName = "Crm.Authorization:010002";

    public const string GivenPolicyHasNotGrantedForGivenResource = "Crm.Authorization:010003";

    public const string GivenRequirementHasNotGrantedForGivenResource = "Crm.Authorization:010004";

    public const string GivenRequirementsHasNotGrantedForGivenResource = "Crm.Authorization:010005";
}