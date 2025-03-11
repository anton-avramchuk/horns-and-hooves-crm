using Microsoft.Extensions.Logging;

namespace HornsAndHoovesCrm.Core.Logging;

public interface IExceptionWithSelfLogging
{
    void Log(ILogger logger);
}