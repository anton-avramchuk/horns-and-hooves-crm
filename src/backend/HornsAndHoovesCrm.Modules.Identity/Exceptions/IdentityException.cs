using HornsAndHoovesCrm.Core.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace HornsAndHoovesCrm.Modules.Identity.Exceptions;

public class IdentityException : CrmException
{
    public override string Message { get; }

    public IdentityException(IdentityResult result)
    {
        Message = string.Join("\n", result.Errors.Select(x => x.Description));
    }
}