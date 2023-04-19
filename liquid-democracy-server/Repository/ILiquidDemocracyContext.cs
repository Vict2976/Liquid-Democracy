namespace Repository;

using Microsoft.EntityFrameworkCore;

public interface ILiquidDemocracyContext : IDisposable
{
    public DbSet<Election> Elections { get; }
    public DbSet<Candidate> Candidates { get; }
    public DbSet<Ballot> Ballots { get; }

    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}