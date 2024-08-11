using System.Collections.Immutable;
using Shared.Domain.Bus.Query;
using SystemAdministrator.LastBackups.Application.Dtos;
using SystemAdministrator.LastBackups.Application.Dtos.Transformation;
using SystemAdministrator.LastBackups.Domain;

namespace SystemAdministrator.LastBackups.Application.BackupsGetLast
{
  public class BackupsGetLastHandler(BackupsGetLast backupsGetLast) : QueryHandler<BackupsGetLastQuery, ImmutableList<BackupDto>>
  {
    private BackupsGetLast _backupsGetLast = backupsGetLast;
    public async Task<ImmutableList<BackupDto>> Handle(BackupsGetLastQuery query, CancellationToken cancellationToken)
    {
      ImmutableList<Backup> backups = await _backupsGetLast.Search();

      ImmutableList<BackupDto> backupDtos = backups.Select(BackupToBackupDTOTransformation.Run).ToImmutableList();

      return backupDtos;
    }
  }
}