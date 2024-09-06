using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;

namespace Clouds.LastBackups.Infrastructure.Repository.MongoDB
{
  public class LastBackupsStatusContext : DbContext
  {
    public DbSet<LastBackupsStatusEntity> LastBackupStatus { get; init; }

    public LastBackupsStatusContext(DbContextOptions options)
        : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);
      modelBuilder.Entity<LastBackupsStatusEntity>().ToCollection("lastBackupStatus");
    }
  }
}