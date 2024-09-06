using Moq;
using Shared.Domain.Bus.Command;
using Shared.Domain.Bus.Event;
using Shared.Domain.Bus.Query;
using SystemAdministrator.Machines.Domain;

namespace SystemAdministrationTest.Machines.Infrastructure
{
  public class BackupsUnitTestCase
  {
    protected Mock<MachinesRepository> _repository = new Mock<MachinesRepository>();
    protected Mock<QueryBus> _queryBusMok = new Mock<QueryBus>();

    protected Mock<EventBus> _eventBusMok = new Mock<EventBus>();

    protected Mock<Consumer> _consumerMok = new Mock<Consumer>();

  }
}