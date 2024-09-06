namespace SystemAdministrator.Machines.Application.Dtos;

public record MachineDto(
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
