using Shared.Domain.Bus.Query;

namespace Shared.Backups.Application.BackupsGetLastNDays
{
    public class BackupsGetByCloudIdQuery (string cloudBackupId): Query
  {
    public string CloudBackupId {get;} = cloudBackupId;
  }
}