namespace HornsAndHoovesCrm.Core.ExceptionHandling;

public interface IHasErrorCode
{
    string? Code { get; }
}