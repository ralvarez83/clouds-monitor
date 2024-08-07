using Shared.Domain.ValueObjects;

namespace SystemAdministrator.LastBackups.Domain.ValueObjects;

public class BackupId : SimpleUuid
{

  public BackupId(Guid id) : base(id) { }
  public BackupId() : base() { }

}