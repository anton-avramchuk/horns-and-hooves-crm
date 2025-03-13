using HornsAndHoovesCrm.Core.Modularity;
using HornsAndHoovesCrm.ObjectMapping.Abstractions;
using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace HornsAndHoovesCrm.ObjectMapping.Mapster;

[DependsOn(typeof(CrmObjectMappingModule))]
public class CrmMapsterModule : CrmModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {

        context.Services.AddSingleton(TypeAdapterConfig.GlobalSettings);
        context.Services.AddScoped(AddMapper);
    }

    private IObjectMapper AddMapper(IServiceProvider provider)
    {
        var config = provider.GetRequiredService<TypeAdapterConfig>();

        provider.GetServices<IMapsterMappingProfile>()
            .ToList()
            .ForEach(profile => profile.Configure(config));


        return new MapsterObjectMapper(config);
    }
}