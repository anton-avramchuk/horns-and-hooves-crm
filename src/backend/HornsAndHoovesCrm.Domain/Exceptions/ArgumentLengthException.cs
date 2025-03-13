using HornsAndHoovesCrm.Core.Exceptions;

namespace HornsAndHoovesCrm.Domain.Exceptions;

public class ArgumentLengthException : CrmException
{

    public ArgumentLengthException(string name,int length):base($"Invalid Length of {name}. MaxLength is {length}")
    {
        Name = name;
        Length = length;
    }

    public string Name { get;private set; }

    public int Length { get;private set; }


}