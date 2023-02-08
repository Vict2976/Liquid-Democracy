namespace Entities;

using Microsoft.EntityFrameworkCore;

public interface ILiquidDemocracyContext : IDisposable
{
    public DbSet<User> Users { get; }
    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}