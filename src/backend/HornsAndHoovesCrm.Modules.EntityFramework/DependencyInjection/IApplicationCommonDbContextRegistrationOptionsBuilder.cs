using Microsoft.Extensions.DependencyInjection;

namespace HornsAndHoovesCrm.Modules.EntityFramework.DependencyInjection;

public interface IApplicationCommonDbContextRegistrationOptionsBuilder
{
    IServiceCollection Services { get; }
}