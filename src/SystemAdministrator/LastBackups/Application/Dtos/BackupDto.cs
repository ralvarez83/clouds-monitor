namespace SystemAdministrator.LastBackups.Application.Dtos;

public record BackupDto(string Id, string CloudBackupId, string name, string Status, DateTime? StartDate, DateTime? EndtDate)
{

}
