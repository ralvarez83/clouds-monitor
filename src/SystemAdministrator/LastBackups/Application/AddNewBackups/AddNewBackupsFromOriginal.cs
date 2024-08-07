using System.Collections.Immutable;
using SystemAdministrator.LastBackups.Application.BackupsGetLast;
using SystemAdministrator.LastBackups.Application.Dtos;
using Shared.Domain.Bus.Command;
using Shared.Domain.Bus.Query;

namespace SystemAdministrator.LastBackups.Application.AddNewBackups
{
  public class AddNewBackupsFromOriginal(CommandBus commandBus, QueryBus queryBus)
  {
    private readonly CommandBus _commandBus = commandBus;
    private readonly QueryBus _queryBus = queryBus;

    public async void Run()
    {
      BackupsGetLastQuery backupsGetLastQuery = new BackupsGetLastQuery();

      ImmutableList<BackupDto> backupsDtos = await _queryBus.Ask<ImmutableList<BackupDto>>(backupsGetLastQuery);

      if (null != backupsDtos)
      {
        foreach (BackupDto backupDto in backupsDtos)
        {
          // _commandRepository.Save(BackupDtoToBackupTransformation.Run(backupDto));
        }
      }
    }

  }
}