namespace HornsAndHoovesCrm.AspNetCore.Jwt.Models;

public class LoginResult
{
    public LoginResult(string token, string userName, string userFullName, string[] roles, string[] claims)
    {
        Token = token ?? throw new ArgumentNullException(nameof(token));
        UserName = userName ?? throw new ArgumentNullException(nameof(userName));
        UserFullName = userFullName ?? throw new ArgumentNullException(nameof(userFullName));
        Roles = roles ?? throw new ArgumentNullException(nameof(roles));
        Claims = claims ?? throw new ArgumentNullException(nameof(claims));
    }

    public string Token { get; }

    public string UserName { get; }
    public string UserFullName { get; }

    public string[] Roles { get; }

    public string[] Claims { get; }
}