using Microsoft.EntityFrameworkCore;
using SystemAdministrator.LastBackups.Application.Dtos;

namespace SystemAdministrationTest.Backups.Infraestructure.InMemoryDB
{
  public class ApplicationDbContext : DbContext
  {
    public DbSet<MachineDto> Backups { get; set; }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
  }
}