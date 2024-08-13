using Clouds.LastBackups.Application.Dtos;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using MongoDB.EntityFrameworkCore.Extensions;

namespace Clouds.LastBackups.Infraestructure.Repository.MongoDB
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