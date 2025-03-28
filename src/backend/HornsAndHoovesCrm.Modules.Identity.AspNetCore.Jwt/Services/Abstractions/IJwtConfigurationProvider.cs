using HornsAndHoovesCrm.Modules.Identity.AspNetCore.Jwt.Options;

namespace HornsAndHoovesCrm.Modules.Identity.AspNetCore.Jwt.Services.Abstractions;

public interface IJwtConfigurationProvider
{
    JwtConfiguration Configuration { get; }
}