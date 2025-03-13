namespace HornsAndHoovesCrm.Modules.Identity.Domain.Shared;

public static class IdentityUserConsts
{
    public static int MaxUserNameLength { get; set; } = 256;


    public static int MaxNormalizedUserNameLength { get; set; } = MaxUserNameLength;

    public static int MaxEmailLength { get; set; } = 256;

    public static int MaxNormalizedEmailLength { get; set; } = MaxEmailLength;



    /// <summary>
    /// Default value: 128
    /// </summary>
    public static int MaxPasswordLength { get; set; } = 128;

    /// <summary>
    /// Default value: 256
    /// </summary>
    public static int MaxPasswordHashLength { get; set; } = 256;

    /// <summary>
    /// Default value: 256
    /// </summary>
    public static int MaxSecurityStampLength { get; set; } = 256;

    /// <summary>
    /// Default value: 16
    /// </summary>
    public static int MaxLoginProviderLength { get; set; } = 16;
}