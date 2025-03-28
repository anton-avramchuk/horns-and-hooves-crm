using HornsAndHoovesCrm.AspNetCore.Jwt.Models;

namespace HornsAndHoovesCrm.Modules.Identity.AspNetCore.Jwt;

public record LoginModel : ILoginModel
{
    public required string UserName { get; set; }
    public required string Password { get; set; }
}