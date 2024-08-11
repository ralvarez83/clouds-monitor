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
  }
}