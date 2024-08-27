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
    private RegisterBackupSubscriber _subscriber;
    public RegisterBackupTest()
    {
      _handler = new RegisterBackupHandler(new RegisterBackup(_repository.Object));
      _subscriber = new RegisterBackupSubscriber(new RegisterBackup(_repository.Object));
    }

    [Fact]
    public void NewBackupShouldSaved()
    {
      // Given the backup is not in repository
      ImmutableList<Machine> backupsInRepository = MachineFactory.BuildArrayOfBackupsRandom();
      ConfigureGetRepositoryGetById(backupsInRepository);


      // When backup is Register
      MachineDomainEvent newBackup = MachineDomainEnventFactory.BuildBackupDtoRandom();
      _subscriber.On(newBackup);

      // Then the save repository method must be callled
      ShouldHaveSave(1);
    }

    [Fact]
    public void MachineIsNotUpdateWithSameBackupData()
    {
      // Given the machine exists in repository and backup data is update
      ImmutableList<Machine> machinesInRepository = MachineFactory.BuildArrayOfBackupsRandom();
      ConfigureGetRepositoryGetById(machinesInRepository);

      // When backup wants to be register with no new data
      MachineDomainEvent machineDomainEvent = MachineDomainEnventFactory.WrapperFromDomain(machinesInRepository.First());
      _subscriber.On(machineDomainEvent);

      // Then the manchine is not updated
      ShouldHaveSave(0);
    }


    [Fact]
    public void MachineIsUpdateWithNewBackup()
    {
      // Given the machine exists in repository but backup data is older
      ImmutableList<Machine> machinesInRepository = MachineFactory.BuildArrayOfBackupsRandom();
      ConfigureGetRepositoryGetById(machinesInRepository);

      // When a new backup is register

      Machine machineInRepository = machinesInRepository.First();

      BackupDate newBackupDate = new BackupDate(machinesInRepository.First().LastBackupTime.Value.AddDays(1));

      MachineDomainEvent machineDomainEventWithNewData = new MachineDomainEvent(machineInRepository.MachineId.Value,
                                                                                machineInRepository.MachineName.Value,
                                                                                machineInRepository.LastBackupStatus.ToString(),
                                                                                newBackupDate.ToString(),
                                                                                machineInRepository.LastBackupType.ToString(),
                                                                                machineInRepository.LastRecoveryPoint.ToString(),
                                                                                machineInRepository.VaultId.Value,
                                                                                machineInRepository.SuscriptionId.Value,
                                                                                machineInRepository.TenantId.Value);

      _subscriber.On(machineDomainEventWithNewData);


      // Then machine backup data is update
      ShouldHaveSaveWithBackupData(MachineWrapper.FromDomainEntity(machineDomainEventWithNewData));
    }

    [Fact]
    public void MachineLastBackupNullIsUpdate()
    {
      // Given the machine existis but hasn't LastBackupRecovery
      ImmutableList<Machine> machinesInRepository = MachineFactory.BuildArrayOfBackupsRandom();
      machinesInRepository.First().LastRecoveryPoint = null;
      ConfigureGetRepositoryGetById(machinesInRepository);

      // When save a new backupData with LastBackupRecovery
      Machine machineInRepository = machinesInRepository.First();

      BackupDate newBackupDate = new BackupDate(machinesInRepository.First().LastBackupTime.Value.AddDays(1));

      MachineDomainEvent machineDomainEventWithNewData = new MachineDomainEvent(machineInRepository.MachineId.Value,
                                                                                machineInRepository.MachineName.Value,
                                                                                machineInRepository.LastBackupStatus.ToString(),
                                                                                newBackupDate.ToString(),
                                                                                machineInRepository.LastBackupType.ToString(),
                                                                                newBackupDate.ToString(),
                                                                                machineInRepository.VaultId.Value,
                                                                                machineInRepository.SuscriptionId.Value,
                                                                                machineInRepository.TenantId.Value);

      _subscriber.On(machineDomainEventWithNewData);

      // Then the machine is updated
      ShouldHaveSaveWithBackupData(MachineWrapper.FromDomainEntity(machineDomainEventWithNewData));
    }

    private void ConfigureGetRepositoryGetById(ImmutableList<Machine> backupsInRepository)
    {
      _repository
        .Setup(_ => _.GetById(
          It.Is<MachineId>(
            machineId => backupsInRepository.Exists(backup => backup.MachineId.Value == machineId.Value))))
        .Returns<MachineId>((MachineId machineId) =>
                {
                  return Task.Run(
                    () => backupsInRepository.First<Machine?>(backup => backup.MachineId.Value == machineId.Value));
                }
        );
    }
  }
}