namespace Repository;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;


public class UserIDGenerator : ValueGenerator<int>
{

    public override bool GeneratesTemporaryValues => false;

    private int GenerateRandomNumber()
    {
        Random random = new Random(); 
        var i = random.Next(11000,2000000000);
        return i;
    }

    public override int Next(EntityEntry entry) => GenerateRandomNumber();
}
public class LiquidDemocracyContext : DbContext, ILiquidDemocracyContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Candidate> Candidates => Set<Candidate>();
    public DbSet<Election> Elections => Set<Election>();
    public DbSet<Vote> Votes => Set<Vote>();
    public DbSet<VoteUsedOn> VoteUsedOns => Set<VoteUsedOn>();
    public DbSet<Block> Blocks => Set<Block>();

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
        .HasKey(v => new { v.VoteUsedOnId });

    modelBuilder.Entity<VoteUsedOn>()
        .HasOne(v => v.Candidate)
        .WithMany(c => c.DelegatedVotes)
        .OnDelete(DeleteBehavior.Restrict);

    modelBuilder.Entity<User>()
        .Property(u => u.UserId).HasValueGenerator<UserIDGenerator>();
    }
    
}
