namespace Repository;

using Microsoft.EntityFrameworkCore;

public interface ILiquidDemocracyContext : IDisposable
{
    public DbSet<User> Users { get; }
    public DbSet<Candidate> Candidates { get; }
    public DbSet<Election> Elections { get; }
    public DbSet<Vote> Votes { get; }
    public DbSet<VoteUsedOn> VoteUsedOns { get; }

    public DbSet<Block> Blocks { get; }

    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}