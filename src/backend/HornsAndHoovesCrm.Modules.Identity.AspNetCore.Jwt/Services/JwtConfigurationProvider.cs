using HornsAndHoovesCrm.Core.DependencyInjection;
using HornsAndHoovesCrm.Modules.Identity.AspNetCore.Jwt.Options;
using HornsAndHoovesCrm.Modules.Identity.AspNetCore.Jwt.Services.Abstractions;
using Microsoft.Extensions.Options;

namespace HornsAndHoovesCrm.Modules.Identity.AspNetCore.Jwt.Services;

public class JwtConfigurationProvider : IJwtConfigurationProvider, ISingletonDependency
{
    public JwtConfigurationProvider(IOptions<JwtConfiguration> options)
    {
        Configuration = options.Value;
    }

    public JwtConfiguration Configuration { get; }
}