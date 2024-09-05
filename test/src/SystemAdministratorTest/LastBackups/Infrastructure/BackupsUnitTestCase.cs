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

    protected Mock<EventBus> _eventBusMok = new Mock<EventBus>();

    protected Mock<Consumer> _consumerMok = new Mock<Consumer>();

  }
}