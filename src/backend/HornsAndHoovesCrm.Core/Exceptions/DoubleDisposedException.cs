namespace HornsAndHoovesCrm.Core.Exceptions;

public class DoubleDisposedException : CrmException
{
    public DoubleDisposedException() : base("Object disposed more once")
    {
            
    }
}