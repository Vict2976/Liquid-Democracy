namespace Entities;

using Microsoft.EntityFrameworkCore;
public class LiquidDemocracyContext : DbContext, ILiquidDemocracyContext
{
    public DbSet<User> Users => Set<User>();

    public LiquidDemocracyContext(DbContextOptions<LiquidDemocracyContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

    }

}
