using HornsAndHoovesCrm.Modules.Identity.Auth.Services;
using HornsAndHoovesCrm.Modules.Identity.Auth.Services.Abstracctions;
using HornsAndHoovesCrm.Modules.Identity.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace HornsAndHoovesCrm.Modules.Identity.Auth.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddIdentity<TIdentityContext, TSignInManager, TUser, TRole>(this IServiceCollection services, Action<IdentityOptions> setupAction)
        where TUser : Domain.IdentityUser<TRole>
        where TRole : Domain.IdentityRole
        where TIdentityContext : IdentityDbContext<TIdentityContext, TUser, TRole>
        where TSignInManager : IdentitySignInManager<TUser, TRole>
    {
        services.AddIdentityContext<TIdentityContext, TUser, TRole>(setupAction)
            .AddSignInManager<TSignInManager>()
            .AddDefaultTokenProviders();
        services.AddScoped<IAuthService, IdentityAuthService<TUser, TRole>>();

        if (typeof(TSignInManager) != typeof(IdentitySignInManager<TUser, TRole>))
        {
            services.AddScoped<IdentitySignInManager<TUser, TRole>, TSignInManager>();
        }

        return services;
    }

    public static IServiceCollection AddIdentity<TIdentityContext, TUser, TRole>(this IServiceCollection services, Action<IdentityOptions> setupAction)
        where TUser : Domain.IdentityUser<TRole>
        where TRole : Domain.IdentityRole
        where TIdentityContext : IdentityDbContext<TIdentityContext, TUser, TRole>
    {


        return services.AddIdentity<TIdentityContext, IdentitySignInManager<TUser, TRole>, TUser, TRole>(setupAction);
    }
}