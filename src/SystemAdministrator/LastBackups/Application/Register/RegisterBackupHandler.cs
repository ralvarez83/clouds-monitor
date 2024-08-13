using Shared.Domain.Bus.Command;
using SystemAdministrator.LastBackups.Application.Dtos.Wrappers;
using SystemAdministrator.LastBackups.Domain;

namespace SystemAdministrator.LastBackups.Application.Register
{
  public class RegisterBackupHandler(RegisterBackup registerBackup) : CommandHandler<RegisterBackupCommand>
  {
    private readonly RegisterBackup registerBackup = registerBackup;

    public Task Handle(RegisterBackupCommand command)
    {
      Backup backup = BackupWrapper.FromDto(command.Backup);
      registerBackup.Register(backup);

      return Task.CompletedTask;
    }
  }
}