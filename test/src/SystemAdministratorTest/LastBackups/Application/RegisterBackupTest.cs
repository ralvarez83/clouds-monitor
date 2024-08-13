using System.Collections.Immutable;
using Moq;
using Shared.Domain.ValueObjects;
using SystemAdministrationTest.Backups.Domain;
using SystemAdministrationTest.LastBackups.Infrastructure;
using SystemAdministrator.LastBackups.Application.Dtos;
using SystemAdministrator.LastBackups.Application.Dtos.Wrappers;
using SystemAdministrator.LastBackups.Application.Register;
using SystemAdministrator.LastBackups.Domain;

namespace SystemAdministrationTest.LastBackup.Application
{
  public class RegisterBackupTest : BackupsUnitTestCase
  {

    private RegisterBackupHandler _handler;
    public RegisterBackupTest()
    {
      _handler = new RegisterBackupHandler(new RegisterBackup(_repository.Object));
    }

    [Fact]
    public void NewBackupShouldSaved()
    {
      // Given the backup is not in repository
      ImmutableList<Backup> backupsInRepository = BackupsFactory.BuildArrayOfBackupsRandom();
      ConfigureGetRepositoryGetById(backupsInRepository);


      // When backup is Register
      BackupDto newBackup = BackupsDtoFactory.BuildBackupDtoRandom();
      RegisterBackupCommand command = new RegisterBackupCommand(newBackup);
      _handler.Handle(command);

      // Then the save repository method must be callled
      ShouldHaveSave(1);
    }

    [Fact]
    public void MachineIsNotUpdateWithSameBackupData()
    {
      // Given the machine exists in repository and backup data is update
      ImmutableList<Backup> backupsInRepository = BackupsFactory.BuildArrayOfBackupsRandom();
      ConfigureGetRepositoryGetById(backupsInRepository);

      // When backup wants to be register with no new data
      BackupDto backupWithNoNewData = BackupDtoWrapper.FromDomain(backupsInRepository.First());
      RegisterBackupCommand command = new RegisterBackupCommand(backupWithNoNewData);
      _handler.Handle(command);

      // Then the manchine is not updated
      ShouldHaveSave(0);
    }




    private void ConfigureGetRepositoryGetById(ImmutableList<Backup> backupsInRepository)
    {
      _repository
        .Setup(_ => _.GetById(
          It.Is<MachineId>(
            machineId => backupsInRepository.Exists(backup => backup.MachineId.Value == machineId.Value))))
        .Returns<MachineId>((MachineId machineId) =>
                {
                  return Task.Run(
                    () => backupsInRepository.First<Backup?>(backup => backup.MachineId.Value == machineId.Value));
                }
        );
    }
  }
}