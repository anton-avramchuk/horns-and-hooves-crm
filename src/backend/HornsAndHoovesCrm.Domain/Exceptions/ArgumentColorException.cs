using HornsAndHoovesCrm.Core.Exceptions;

namespace HornsAndHoovesCrm.Domain.Exceptions;

public class ArgumentColorException : CrmException
{
    public ArgumentColorException(string parameterName) :base($"\"Color must be a valid hex value.\", {parameterName}")
    {
            
    }
}