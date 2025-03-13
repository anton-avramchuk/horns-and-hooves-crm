using HornsAndHoovesCrm.Domain.Exceptions;

namespace HornsAndHoovesCrm.Domain.Validators;

public static class StringValidators
{
    public static void ValidateStringIsNotNull(string? value, string parameterName)
    {
        if (string.IsNullOrEmpty(value))
            throw new ArgumentNullException(parameterName);
    }


    public static void ValidateStringLength(string? value, string parameterName, int length)
    {
        if (value is null)
            return;
        if (value.Length > length)
            throw new ArgumentLengthException(parameterName, length);
    }



    public static void ValidateStrinIsHexColor(string value, string parameterName)
    {
        if (!System.Text.RegularExpressions.Regex.IsMatch(value, "^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{8})$"))
        {
            throw new ArgumentColorException(parameterName);
        }
    }

}