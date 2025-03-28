namespace HornsAndHoovesCrm.Modules.Identity.AspNetCore.Jwt.Options;

public class JwtConfiguration
{
    public int TokenLifetimeMinutes { get; set; }

    public required string Secret { get; set; }
}