using Microsoft.Extensions.DependencyInjection;

namespace HornsAndHoovesCrm.ObjectMapping.Mapster.Extensions;

public static class RegistrationExtensions
{
    public static IServiceCollection AddMapping<TMapping>(this IServiceCollection services) where TMapping : class, IMapsterMappingProfile
    {
        services.AddScoped<IMapsterMappingProfile, TMapping>();
        return services;
    }
}