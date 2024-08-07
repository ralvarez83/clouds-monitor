using Azure.ResourceManager.RecoveryServicesBackup.Models;
using Clouds.LastBackups.Application.Dtos;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using MongoDB.EntityFrameworkCore.Extensions;

namespace Clouds.LastBackups.Infraestructure.Repository.MongoDB
{
  public class LastBackupsStatusContext : DbContext
  {
    public DbSet<LastBackupStatusDto> LastBackupStatus { get; init; }
    public static LastBackupsStatusContext Create(IMongoDatabase database) =>
        new(new DbContextOptionsBuilder<LastBackupsStatusContext>()
            .UseMongoDB(database.Client, database.DatabaseNamespace.DatabaseName)
            .Options);
    public LastBackupsStatusContext(DbContextOptions options)
        : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);
      modelBuilder.Entity<LastBackupStatusDto>().ToCollection("lastBackupStatus");
    }
  }
}