namespace Repository;

using Microsoft.EntityFrameworkCore;
public class LiquidDemocracyContext : DbContext, ILiquidDemocracyContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Candidate> Candidates => Set<Candidate>();
    public DbSet<Election> Elections => Set<Election>();
    public DbSet<Vote> Votes => Set<Vote>();
    public DbSet<VoteUsedOn> VoteUsedOns => Set<VoteUsedOn>();

    public LiquidDemocracyContext(DbContextOptions<LiquidDemocracyContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    modelBuilder.Entity<Vote>()
        .HasOne(v => v.User)
        .WithMany(u => u.Votes)
        .HasForeignKey(v => v.BelongsToId)
        .OnDelete(DeleteBehavior.Restrict);

    modelBuilder.Entity<Vote>()
        .HasOne(v => v.Election)
        .WithMany(e => e.Votes)
        .HasForeignKey(v => v.ElectionId)
        .OnDelete(DeleteBehavior.Cascade);

    modelBuilder.Entity<Candidate>()
        .HasOne(c => c.Election)
        .WithMany(e => e.Candidates)
        .HasForeignKey(c => c.ElectionId)
        .OnDelete(DeleteBehavior.Cascade);

    modelBuilder.Entity<VoteUsedOn>()
        .HasKey(v => new { v.VoteId });

    modelBuilder.Entity<VoteUsedOn>()
        .HasOne(v => v.Vote)
        .WithMany(v => v.DelegatedVotes)
        .HasForeignKey(v => v.VoteId)
        .OnDelete(DeleteBehavior.Cascade);

    modelBuilder.Entity<VoteUsedOn>()
        .HasOne(v => v.Candidate)
        .WithMany(c => c.DelegatedVotes)
        .OnDelete(DeleteBehavior.Restrict);

    modelBuilder.Entity<VoteUsedOn>()
        .HasOne(v => v.Delegate)
        .WithMany(u => u.DelegatedVotes);
    }

}
