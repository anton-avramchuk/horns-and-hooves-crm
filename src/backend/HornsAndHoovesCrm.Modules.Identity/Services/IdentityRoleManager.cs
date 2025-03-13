using HornsAndHoovesCrm.Modules.Identity.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace HornsAndHoovesCrm.Modules.Identity.Services;

public class IdentityRoleManager<TIdentityRole> : RoleManager<TIdentityRole> where TIdentityRole : IdentityRole
{
    public IdentityRoleManager(IRoleStore<TIdentityRole> store, IEnumerable<IRoleValidator<TIdentityRole>> roleValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, ILogger<RoleManager<TIdentityRole>> logger) : base(store, roleValidators, keyNormalizer, errors, logger)
    {
    }


    public virtual async Task<TIdentityRole?> GetByIdAsync(Guid id)
    {
        var role = await Store.FindByIdAsync(id.ToString(), CancellationToken);
        

        return role;
    }
      
}