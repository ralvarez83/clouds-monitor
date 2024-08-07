using Shared.Domain.ValueObjects;

namespace Clouds.LastBackups.Domain.ValueObjects;

public class BackupId : SimpleUuid
{

  public BackupId(Guid id) : base(id) { }
  public BackupId() : base() { }

  public static new string GetName() => "BackupId";

}