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
    public DbSet<Election> Elections => Set<Election>();
    public DbSet<Candidate> Candidates => Set<Candidate>();
    public DbSet<Ballot> Ballots => Set<Ballot>();
    public DbSet<Tally> Tallies => Set<Tally>();

    public LiquidDemocracyContext(DbContextOptions<LiquidDemocracyContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

    modelBuilder.Entity<Candidate>()
        .HasOne(c => c.Election)
        .WithMany(e => e.Candidates)
        .HasForeignKey(c => c.ElectionId)
        .OnDelete(DeleteBehavior.Cascade);

    }
    
}
