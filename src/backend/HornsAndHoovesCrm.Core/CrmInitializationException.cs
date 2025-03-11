using HornsAndHoovesCrm.Core.Exceptions;

namespace HornsAndHoovesCrm.Core;

public class CrmInitializationException : CrmException
{
    

    public CrmInitializationException(string message)
        : base(message)
    {

    }

    public CrmInitializationException(string message, Exception innerException)
        : base(message, innerException)
    {

    }

}