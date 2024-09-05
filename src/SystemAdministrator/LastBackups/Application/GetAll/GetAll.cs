using System.Collections.Immutable;
using RabbitMQ.Client.Events;
using SystemAdministrator.LastBackups.Domain;

namespace SystemAdministrator.LastBackups.Application.GetAll
{
  public class GetAll(LastBackupsRepository repository)
  {
    private readonly LastBackupsRepository repository = repository;

    public async Task<ImmutableList<Machine>> Run()
    {
      return await repository.GetAll();
    }
  }
}