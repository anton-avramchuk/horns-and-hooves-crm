using HornsAndHoovesCrm.Modules.EntityFramework;
using HornsAndHoovesCrm.Modules.Identity.Domain;
using Microsoft.EntityFrameworkCore;

namespace HornsAndHoovesCrm.Modules.Identity;

public interface IIdentityDbContext : ICrmDbContext
{

}

public abstract class IdentityDbContext<TDbContext, TIdentityUser, TIdentityRole> : CrmDbContext<TDbContext>, IIdentityDbContext
    where TIdentityUser : IdentityUser<TIdentityRole> where TIdentityRole : IdentityRole where TDbContext : DbContext
{
    protected IdentityDbContext(DbContextOptions<TDbContext> options) : base(options)
    {
    }


}