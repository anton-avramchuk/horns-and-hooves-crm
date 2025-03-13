using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace HornsAndHoovesCrm.AspNetCore.Jwt.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddJwt(this IServiceCollection services,string secret,string issuer)
    {
        var key = Encoding.UTF8.GetBytes(secret);
        services.AddAuthentication(w =>
        {
            w.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            w.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            w.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(w =>
        {
            w.RequireHttpsMetadata = false;
            w.SaveToken = true;
            w.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero,
                ValidIssuer = issuer
            };
        });
        services.AddAuthorization();
    }
}