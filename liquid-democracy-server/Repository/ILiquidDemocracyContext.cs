namespace Repository;

using Microsoft.EntityFrameworkCore;

public interface ILiquidDemocracyContext : IDisposable
{
    public DbSet<Election> Elections { get; }
    public DbSet<Candidate> Candidates { get; }
    public DbSet<Ballot> Ballots { get; }
    public DbSet<Tally> Tallies { get; }

    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}