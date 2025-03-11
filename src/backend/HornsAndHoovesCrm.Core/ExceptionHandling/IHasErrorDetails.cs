namespace HornsAndHoovesCrm.Core.ExceptionHandling;

public interface IHasErrorDetails
{
    string? Details { get; }
}