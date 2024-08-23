using Microsoft.EntityFrameworkCore;
using SystemAdministrator.LastBackups.Application.Dtos;

namespace SystemAdministrationTest.Backups.Infraestructure.InMemoryDB
{
  public class ApplicationDbContext : DbContext
  {
    public DbSet<BackupDto> Backups { get; set; }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
  }
}