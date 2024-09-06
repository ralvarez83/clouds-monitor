using System.Collections.Immutable;
using SystemAdministrationTest.Machines.Infrastructure;
using SystemAdministrator.Machines.Application.GetAll;
using SystemAdministrator.Machines.Application.Dtos;
using SystemAdministrator.Machines.Domain;
using SystemAdministrationTest.Machines.Domain;
using SystemAdministrator.Machines.Application.Dtos.Wrappers;

namespace SystemAdministrationTest.Machines.Application
{
  public class GetAllTest : BackupsUnitTestCase
  {
    private GetAllHandler _handler;

    public GetAllTest()
    {

      _handler = new GetAllHandler(new GetAll(_repository.Object));
    }

    [Fact]
    public async Task RepositoryIsEmtpy_Then_ShouldReturnAnEmptyListAsync()
    {
      // Given database is empty
      ImmutableList<Machine> empty = [];
      ConfigureRepositoryGetAll(empty);

      // When get all machines
      ImmutableList<MachineDto> machinesReturned = await _handler.Handle(new GetAllQuery(), CancellationToken.None);

      // Then the list is empty
      Assert.Empty(machinesReturned);
    }

    [Fact]
    public async Task RepositoryHasMachinesData_Then_AllOfThemShouldBeReturnedAsync()
    {
      // Given repository has Machine Info
      ImmutableList<Machine> machinesInRepository = MachineFactory.BuildArrayOfBackupsRandom();
      ConfigureRepositoryGetAll(machinesInRepository);

      // When get all machines
      ImmutableList<MachineDto> machinesReturned = await _handler.Handle(new GetAllQuery(), CancellationToken.None);

      // Then all machinas should be returned
      Assert.Equal(machinesInRepository.Select(MachineDtoWrapper.FromDomain), machinesReturned);
    }


    private void ConfigureRepositoryGetAll(ImmutableList<Machine> machinesInRepository)
    {
      _repository
        .Setup(_ => _.GetAll())
        .Returns(() =>
        {
          return Task.Run(() => machinesInRepository);
        });
    }
  }
}