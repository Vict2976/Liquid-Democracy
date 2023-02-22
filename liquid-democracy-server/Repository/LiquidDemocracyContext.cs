namespace Repository;

using Microsoft.EntityFrameworkCore;
public class LiquidDemocracyContext : DbContext, ILiquidDemocracyContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Candidate> Candidates => Set<Candidate>();
    public DbSet<Election> Elections => Set<Election>();
    public DbSet<Votings> Votings => Set<Votings>();

    public LiquidDemocracyContext(DbContextOptions<LiquidDemocracyContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
         modelBuilder
        .Entity<Votings>()
        .HasOne(e => e.Election)
        .WithMany(e => e.Votings)
        .OnDelete(DeleteBehavior.ClientCascade);

        modelBuilder
        .Entity<Votings>()
        .HasOne(e => e.User)
        .WithMany(e => e.Votings)
        .OnDelete(DeleteBehavior.ClientCascade);
    }

}
