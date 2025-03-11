using HornsAndHoovesCrm.Core.ExceptionHandling;
using HornsAndHoovesCrm.Core.Exceptions;
using HornsAndHoovesCrm.Core.Logging;
using Microsoft.Extensions.Logging;

namespace HornsAndHoovesCrm.Security.Exceptions;

/// <summary>
/// This exception is thrown on an unauthorized request.
/// </summary>
public class CrmAuthorizationException : CrmException, IHasLogLevel, IHasErrorCode
{
    /// <summary>
    /// Severity of the exception.
    /// Default: Warn.
    /// </summary>
    public LogLevel LogLevel { get; set; }

    /// <summary>
    /// Error code.
    /// </summary>
    public string? Code { get; }

    /// <summary>
    /// Creates a new <see cref="CrmAuthorizationException"/> object.
    /// </summary>
    public CrmAuthorizationException()
    {
        LogLevel = LogLevel.Warning;
    }

    /// <summary>
    /// Creates a new <see cref="CrmAuthorizationException"/> object.
    /// </summary>
    /// <param name="message">Exception message</param>
    public CrmAuthorizationException(string message)
        : base(message)
    {
        LogLevel = LogLevel.Warning;
    }

    /// <summary>
    /// Creates a new <see cref="CrmAuthorizationException"/> object.
    /// </summary>
    /// <param name="message">Exception message</param>
    /// <param name="innerException">Inner exception</param>
    public CrmAuthorizationException(string message, Exception innerException)
        : base(message, innerException)
    {
        LogLevel = LogLevel.Warning;
    }

    /// <summary>
    /// Creates a new <see cref="CrmAuthorizationException"/> object.
    /// </summary>
    /// <param name="message">Exception message</param>
    /// <param name="code">Exception code</param>
    /// <param name="innerException">Inner exception</param>
    public CrmAuthorizationException(string? message = null, string? code = null, Exception? innerException = null)
        : base(message, innerException)
    {
        Code = code;
        LogLevel = LogLevel.Warning;
    }

    public CrmAuthorizationException WithData(string name, object value)
    {
        Data[name] = value;
        return this;
    }
}