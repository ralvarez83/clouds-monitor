using Shared.Domain.Bus.Event;
using Shared.Domain.Machines.Domain;
using SystemAdministrator.LastBackups.Application.Dtos.Wrappers;
using SystemAdministrator.LastBackups.Domain;

namespace SystemAdministrator.LastBackups.Application.Register
{
  public class RegisterBackupSubscriber(RegisterBackup registerBackup) : Subscriber<LastBackupStatusDomainEvent>
  {
    private readonly RegisterBackup registerBackup = registerBackup;
    public Task On(DomainEvent domainEvent)
    {
      Machine machine = MachineWrapper.FromDomainEntity((LastBackupStatusDomainEvent)domainEvent);
      registerBackup.Register(machine);

      return Task.CompletedTask;
    }
  }
}