namespace HornsAndHoovesCrm.Modules.EntityFramework.Providers;

public interface IDbContextProvider<TDbContext>
    where TDbContext : ICrmDbContext
{
    TDbContext GetDbContext();

    Task<TDbContext> GetDbContextAsync();
}