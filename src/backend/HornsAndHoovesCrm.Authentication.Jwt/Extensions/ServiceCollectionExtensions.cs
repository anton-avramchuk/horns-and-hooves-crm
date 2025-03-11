using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;

namespace HornsAndHoovesCrm.Authentication.Jwt.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAuthenticationJwt(this IServiceCollection services)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme);
        return services;
    }
}