namespace SystemAdministrator.LastBackups.Application.Dtos;

public record BackupDto(
                          string machineId,
                          string machineName,
                          string status,
                          DateTime? backupTime,
                          string backupType,
                          DateTime? lastRecoveryPoint,
                          string vaultId,
                          string suscriptionId,
                          string TenantId
                      )
{

}
