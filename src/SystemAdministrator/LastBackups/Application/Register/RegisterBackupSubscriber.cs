using Shared.Domain.Bus.Event;
using SystemAdministrator.LastBackups.Application.Dtos.Wrappers;
using SystemAdministrator.LastBackups.Domain;

namespace SystemAdministrator.LastBackups.Application.Register
{
  public class RegisterBackupSubscriber(RegisterBackup registerBackup) : Subscriber
  {
    private readonly RegisterBackup registerBackup = registerBackup;
    public Task On(DomainEvent domainEvent)
    {
      Machine machine = MachineWrapper.FromDomainEntity((MachineDomainEvent)domainEvent);
      registerBackup.Register(machine);

      return Task.CompletedTask;
    }
  }
}