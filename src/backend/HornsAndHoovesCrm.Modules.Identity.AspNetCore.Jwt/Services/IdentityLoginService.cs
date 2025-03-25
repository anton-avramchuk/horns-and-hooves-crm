using HornsAndHoovesCrm.AspNetCore.Jwt.Models;
using HornsAndHoovesCrm.AspNetCore.Jwt.Services;

namespace HornsAndHoovesCrm.Modules.Identity.AspNetCore.Jwt.Services;

public class IdentityLoginService : ILoginService<LoginModel>
{
    public Task<LoginResult?> LoginAsync(LoginModel loginModel)
    {
        return Task.FromResult<LoginResult?>(null);
    }
}

public record LoginModel : ILoginModel
{
    public required string UserName { get; set; }
    public required string Password { get; set; }
}