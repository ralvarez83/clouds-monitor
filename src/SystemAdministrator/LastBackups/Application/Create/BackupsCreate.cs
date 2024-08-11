

using System.Collections.Immutable;
using SystemAdministrator.LastBackups.Application.BackupsGetLast;
using SystemAdministrator.LastBackups.Application.Dtos;
using SystemAdministrator.LastBackups.Application.Dtos.Transformation;
using Shared.Domain.Bus.Query;
using SystemAdministrator.LastBackups.Domain;

namespace SystemAdministrator.LastBackups.Application.AddNewBackups
{
  public class BackupsCreate(LastBackupsRepository commandRepository, QueryBus queryBus)
  {
    private readonly LastBackupsRepository _commandRepository = commandRepository;
    private readonly QueryBus _queryBus = queryBus;

    public async void Run()
    {
      BackupsGetLastQuery backupsGetLastQuery = new BackupsGetLastQuery();

      ImmutableList<BackupDto> backupsDtos = await _queryBus.Ask<ImmutableList<BackupDto>>(backupsGetLastQuery);

      if (null != backupsDtos)
      {
        foreach (BackupDto backupDto in backupsDtos)
        {
          _commandRepository.Save(BackupDtoToBackupTransformation.Run(backupDto));
        }
      }
    }

  }
}