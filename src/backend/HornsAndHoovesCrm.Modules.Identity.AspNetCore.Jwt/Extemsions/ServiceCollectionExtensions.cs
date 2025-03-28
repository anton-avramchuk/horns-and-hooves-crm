using HornsAndHoovesCrm.AspNetCore.Jwt.Services;
using HornsAndHoovesCrm.Modules.Identity.AspNetCore.Jwt.Services;
using HornsAndHoovesCrm.Modules.Identity.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace HornsAndHoovesCrm.Modules.Identity.AspNetCore.Jwt.Extemsions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddIdentityJwtAuth<TIdentityDbContext,TIdentityUser,TIdentityRole>(this IServiceCollection services)
    where TIdentityDbContext: IdentityDbContext<TIdentityDbContext, TIdentityUser, TIdentityRole>
    where TIdentityUser : CrmIdentityUser<TIdentityRole>
    where TIdentityRole : CrmIdentityRole
    {
        services.AddScoped<ILoginService<LoginModel>, IdentityLoginService<TIdentityUser,TIdentityRole>>();
        //services.AddIdentity<>()
        return services;
    }
}