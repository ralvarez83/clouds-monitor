using Shared.Domain.Bus.Event;
using Shared.Domain.Machines.Domain;
using SystemAdministrator.Machines.Application.Dtos.Wrappers;
using SystemAdministrator.Machines.Domain;

namespace SystemAdministrator.Machines.Application.RegisterLastBackups
{
  public class RegisterLastBackupSubscriber(RegisterLastBackup registerBackup) : Subscriber<LastBackupStatusDomainEvent>
  {
    private readonly RegisterLastBackup registerBackup = registerBackup;
    public Task On(DomainEvent domainEvent)
    {
      Machine machine = MachineWrapper.FromDomainEntity((LastBackupStatusDomainEvent)domainEvent);
      registerBackup.Register(machine);

      return Task.CompletedTask;
    }
  }
}