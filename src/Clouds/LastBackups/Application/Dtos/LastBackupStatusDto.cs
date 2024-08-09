namespace Clouds.LastBackups.Application.Dtos;

public record LastBackupStatusDto(
  string MachineId,
  string MachineName,
  string Status,
  DateTime? BackupTime,
  string BackupType,
  DateTime? LastRecoveryPoint,
  string VaultId,
  string SuscriptionId,
  string TenantId
  )
{

}
