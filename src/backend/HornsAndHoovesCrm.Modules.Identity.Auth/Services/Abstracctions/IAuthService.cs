namespace HornsAndHoovesCrm.Modules.Identity.Auth.Services.Abstracctions;

public interface IAuthService
{
    Task<SignInResult> SignIn(string userName, string password);
}

