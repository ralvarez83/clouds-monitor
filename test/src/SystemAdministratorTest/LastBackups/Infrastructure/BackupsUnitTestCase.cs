using Moq;
using Shared.Domain.Bus.Command;
using Shared.Domain.Bus.Event;
using Shared.Domain.Bus.Query;
using SystemAdministrator.LastBackups.Domain;

namespace SystemAdministrationTest.LastBackups.Infrastructure
{
  public class BackupsUnitTestCase
  {
    protected Mock<BackupsRepository> _repository = new Mock<BackupsRepository>();
    protected Mock<QueryBus> _queryBusMok = new Mock<QueryBus>();
    protected Mock<CommandBus> _commandBusMok = new Mock<CommandBus>();

    protected Mock<EventBus> _eventBusMok = new Mock<EventBus>();

    public void ShouldHavePublished(int items, int times)
    {
      _eventBusMok.Verify(x => x.Publish(It.Is<List<DomainEvent>>(list => list.Count == items)), Times.Exactly(times));
    }

    public void ShouldHaveNotPublished()
    {
      _eventBusMok.Verify(x => x.Publish(It.IsAny<List<DomainEvent>>()), Times.Never);
    }

    public void ShouldHaveSave(int? times = null)
    {
      if (times.HasValue)
      {
        if (times.Value == 0)
          _repository.Verify(_ => _.Save(It.IsAny<Backup>()), Times.Never);
        else
          _repository.Verify(_ => _.Save(It.IsAny<Backup>()), Times.Exactly(times.Value));
      }
      else
      {
        _repository.Verify(_ => _.Save(It.IsAny<Backup>()), Times.AtLeastOnce());
      }
    }
    public void ShouldHaveSaveWithBackupData(Backup backupSaved)
    {

      _repository.Verify(_ => _.Save(It.Is<Backup>(
        (backup) => backup.MachineId.Value == backupSaved.MachineId.Value &&
                    backup.MachineName.Value == backupSaved.MachineName.Value &&
                    backup.BackupTime.Value.ToString() == backupSaved.BackupTime.Value.ToString() &&
                    backup.BackupType.Equals(backup.BackupType) &&
                    backup.Status.Equals(backupSaved.Status) &&
                    backup.LastRecoveryPoint.Value.ToString() == backupSaved.LastRecoveryPoint.Value.ToString() &&
                    backup.VaultId.Value == backupSaved.VaultId.Value &&
                    backup.SuscriptionId.Value == backupSaved.SuscriptionId.Value &&
                    backup.TenantId.Value == backupSaved.TenantId.Value
      )), Times.Exactly(1));

    }
  }
}