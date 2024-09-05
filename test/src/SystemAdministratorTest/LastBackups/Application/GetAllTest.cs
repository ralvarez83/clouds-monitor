using System.Collections.Immutable;
using Moq;
using Shared.Domain.ValueObjects;
using SystemAdministrationTest.LastBackups.Infrastructure;
using SystemAdministrator.LastBackups.Application.GetAll;
using SystemAdministrator.LastBackups.Domain;

namespace SystemAdministrationTest.LastBackup.Application
{
  public class GetAllTest : BackupsUnitTestCase
  {
    [Fact]
    public void DatabaseIsEmtpy_Then_ShouldReturnAnEmptyList()
    {
      // Given database is empty
      ImmutableList<Machine> empty = [];
      ConfigureRepositoryGetAll(empty);

      // When get all machines
      ImmutableList<Machine>? machinesReturned = null;
      // machinesReturned = _queryBusMok.Object.Ask(new GetAllQuery());

      // Then the list is empty
      Assert.Empty(machinesReturned);
    }


    private void ConfigureRepositoryGetAll(ImmutableList<Machine> machinesInRepository)
    {
      _repository
        .Setup(_ => _.GetAll())
        .Returns(() => machinesInRepository);
    }
  }
}