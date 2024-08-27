using Moq;
using Shared.Domain.Bus.Command;
using Shared.Domain.Bus.Event;
using Shared.Domain.Bus.Query;
using SystemAdministrator.LastBackups.Domain;

namespace SystemAdministrationTest.LastBackups.Infrastructure
{
  public class BackupsUnitTestCase
  {
    protected Mock<LastBackupsRepository> _repository = new Mock<LastBackupsRepository>();
    protected Mock<QueryBus> _queryBusMok = new Mock<QueryBus>();
    protected Mock<CommandBus> _commandBusMok = new Mock<CommandBus>();

    protected Mock<EventBus> _eventBusMok = new Mock<EventBus>();

    protected Mock<Consumer> _consumerMok = new Mock<Consumer>();

    public void ShouldHavePublished(int items, int times)
    {
      _eventBusMok.Verify(x => x.Publish(It.Is<List<DomainEventPublisher>>(list => list.Count == items)), Times.Exactly(times));
    }

    public void ShouldHaveNotPublished()
    {
      _eventBusMok.Verify(x => x.Publish(It.IsAny<List<DomainEventPublisher>>()), Times.Never);
    }

    public void ShouldHaveSave(int? times = null)
    {
      if (times.HasValue)
      {
        if (times.Value == 0)
          _repository.Verify(_ => _.Save(It.IsAny<Machine>()), Times.Never);
        else
          _repository.Verify(_ => _.Save(It.IsAny<Machine>()), Times.Exactly(times.Value));
      }
      else
      {
        _repository.Verify(_ => _.Save(It.IsAny<Machine>()), Times.AtLeastOnce());
      }
    }
    public void ShouldHaveSaveWithBackupData(Machine backupSaved)
    {

      _repository.Verify(_ => _.Save(It.Is<Machine>(
        (backup) => backup.MachineId.Value == backupSaved.MachineId.Value &&
                    backup.MachineName.Value == backupSaved.MachineName.Value &&
                    backup.LastBackupTime.Value.ToString() == backupSaved.LastBackupTime.Value.ToString() &&
                    backup.LastBackupType.Equals(backup.LastBackupType) &&
                    backup.LastBackupStatus.Equals(backupSaved.LastBackupStatus) &&
                    backup.LastRecoveryPoint.Equals(backupSaved.LastRecoveryPoint) &&
                    backup.VaultId.Value == backupSaved.VaultId.Value &&
                    backup.SuscriptionId.Value == backupSaved.SuscriptionId.Value &&
                    backup.TenantId.Value == backupSaved.TenantId.Value
      )), Times.Exactly(1));

    }
  }
}