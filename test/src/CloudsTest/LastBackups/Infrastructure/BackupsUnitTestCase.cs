using Clouds.LastBackups.Domain;
using Moq;
using Shared.Domain.Bus.Command;
using Shared.Domain.Bus.Event;
using Shared.Domain.Bus.Query;
using Xunit.Sdk;

namespace CloudsTest.LastBackups.Infrastructure
{
  public class BackupsUnitTestCase
  {
    protected Mock<LastBackupsCloudAccess> _cloudAccess = new Mock<LastBackupsCloudAccess>();
    protected Mock<LastBackupsRepository> _repository = new Mock<LastBackupsRepository>();
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
          _repository.Verify(_ => _.Save(It.IsAny<LastBackupStatus>()), Times.Never);
        else
          _repository.Verify(_ => _.Save(It.IsAny<LastBackupStatus>()), Times.Exactly(times.Value));
      }
      else
      {
        _repository.Verify(_ => _.Save(It.IsAny<LastBackupStatus>()), Times.AtLeastOnce());
      }
    }
  }
}