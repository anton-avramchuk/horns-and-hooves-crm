namespace HornsAndHoovesCrm.Modules.Identity.Auth.Services.Abstracctions;

public enum SignInResult
{
    Success,
    InvalidCredentials,
    LockedOut,
    RequiresTwoFactor,
    NotAllowed
}