namespace HornsAndHoovesCrm.Core.ExceptionHandling;

public interface IHasHttpStatusCode
{
    int HttpStatusCode { get; }
}