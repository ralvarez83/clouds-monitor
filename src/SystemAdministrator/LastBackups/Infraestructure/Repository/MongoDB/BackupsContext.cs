using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;

namespace SystemAdministrator.LastBackups.Infrastructure.Repository.MongoDB
{
  public class BackupsContext : DbContext
  {
    public DbSet<BackupsEntity> Backups { get; init; }

    public BackupsContext(DbContextOptions options)
        : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);
      modelBuilder.Entity<BackupsEntity>().ToCollection("lastBackups");
    }
  }
}