using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;

namespace SystemAdministrator.Machines.Infrastructure.Repository.MongoDB
{
  public class MachinesContext : DbContext
  {
    public DbSet<MachinesEntity> Backups { get; init; }

    public MachinesContext(DbContextOptions options)
        : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);
      modelBuilder.Entity<MachinesEntity>().ToCollection("Machines");
    }
  }
}