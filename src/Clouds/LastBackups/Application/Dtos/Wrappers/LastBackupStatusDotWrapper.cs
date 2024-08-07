using Clouds.LastBackups.Domain;
using Shared.Domain.Dtos;

namespace Clouds.LastBackups.Application.Dtos.Transformation;

public class LastBackupStatusDtoWrapper : DtoWrapperFromDomain<LastBackupStatusDto, LastBackupStatus>
{
  public static LastBackupStatusDto FromDomain(LastBackupStatus backup)
  {
    return new LastBackupStatusDto(
      backup.Id.Value.ToString(),
      backup.MachineId.Value,
      backup.MachineName.Value,
      backup.Status.ToString(),
      null != backup.BackupTime ? backup.BackupTime.Value : null,
      backup.BackupType.ToString(),
      null != backup.LastRecoveryPoint ? backup.LastRecoveryPoint.Value : null,
      backup.VaultId.Value,
      backup.SuscriptionId.Value,
      backup.TenantId.Value
    );
  }
}