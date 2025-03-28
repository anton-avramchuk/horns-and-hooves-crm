using HornsAndHoovesCrm.Modules.Identity.Auth.Services.Abstracctions;

namespace HornsAndHoovesCrm.Modules.Identity.Auth.Services;

public class IdentityAuthService<TIdentityUser, TIdentityRole> : IAuthService
    where TIdentityUser : Domain.CrmIdentityUser<TIdentityRole>
    where TIdentityRole : Domain.CrmIdentityRole
{
    private readonly IdentitySignInManager<TIdentityUser, TIdentityRole> _signInManager;
    

    public IdentityAuthService(IdentitySignInManager<TIdentityUser, TIdentityRole> signInManager)
    {
        _signInManager = signInManager;
    }

    public async Task<SignInResult> SignIn(string userName, string password)
    { 
        var result = await _signInManager.PasswordSignInAsync(userName, password, false, lockoutOnFailure: false);

        if (result.Succeeded)
        {
            return SignInResult.Success;
        }
        else if (result.IsLockedOut)
        {
            // Обработка ситуации, когда учетная запись заблокирована
            return SignInResult.LockedOut;
        }
        else if (result.RequiresTwoFactor)
        {
            // Обработка ситуации, когда требуется двухфакторная аутентификация
            return SignInResult.RequiresTwoFactor;
        }
        else if (result.IsNotAllowed)
        {
            // Обработка ситуации, когда пользователь не разрешен для входа
            return SignInResult.NotAllowed;
        }

        // Обработка других возможных сценариев, когда вход не выполнен
        return SignInResult.InvalidCredentials;

    }
}