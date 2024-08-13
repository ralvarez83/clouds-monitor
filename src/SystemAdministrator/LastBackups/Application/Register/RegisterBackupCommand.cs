using Shared.Domain.Bus.Command;
using SystemAdministrator.LastBackups.Application.Dtos;

namespace SystemAdministrator.LastBackups.Application.Register
{
  public class RegisterBackupCommand(BackupDto backup) : Command
  {
    public BackupDto Backup { get; } = backup;
  }
}