using HornsAndHoovesCrm.Modules.Identity.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace HornsAndHoovesCrm.Modules.Identity.Services;

public class IdentityUserManager<TIdentityUser, TIdentityRole> : UserManager<TIdentityUser> where TIdentityRole : CrmIdentityRole where TIdentityUser : CrmIdentityUser<TIdentityRole>
{
    public IdentityUserManager(IUserStore<TIdentityUser> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<TIdentityUser> passwordHasher, IEnumerable<IUserValidator<TIdentityUser>> userValidators, IEnumerable<IPasswordValidator<TIdentityUser>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<TIdentityUser>> logger) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
    {
    }
}