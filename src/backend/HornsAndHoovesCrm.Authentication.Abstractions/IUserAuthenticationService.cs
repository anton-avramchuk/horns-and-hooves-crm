namespace HornsAndHoovesCrm.Authentication.Abstractions;

public interface IUserAuthenticationService
{
    Task<IAuthenticatedUser?> AuthenticateAsync(string login, string password);
}