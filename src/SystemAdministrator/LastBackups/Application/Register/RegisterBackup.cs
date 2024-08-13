using SystemAdministrator.LastBackups.Domain;

namespace SystemAdministrator.LastBackups.Application.Register
{
  public class RegisterBackup(LastBackupsRepository repository)
  {
    private readonly LastBackupsRepository repository = repository;

    public void Register(Backup backup)
    {

      this.repository.Save(backup);

    }
  }
}