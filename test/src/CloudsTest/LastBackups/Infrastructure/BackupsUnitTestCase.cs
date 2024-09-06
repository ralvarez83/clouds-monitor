using Clouds.LastBackups.Domain;
using Moq;
using Shared.Domain.Bus.Command;
using Shared.Domain.Bus.Event;
using Shared.Domain.Bus.Query;

namespace CloudsTest.LastBackups.Infrastructure
{
  public class BackupsUnitTestCase
  {
    protected Mock<LastBackupsCloudAccess> _cloudAccess = new Mock<LastBackupsCloudAccess>();
    protected Mock<LastBackupsRepository> _repository = new Mock<LastBackupsRepository>();
    protected Mock<QueryBus> _queryBusMok = new Mock<QueryBus>();
    protected Mock<CommandBus> _commandBusMok = new Mock<CommandBus>();

    protected Mock<EventBus> _eventBusMok = new Mock<EventBus>();

  }
}