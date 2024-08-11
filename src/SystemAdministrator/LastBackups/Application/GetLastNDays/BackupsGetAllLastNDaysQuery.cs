using Shared.Domain.Bus.Query;

namespace SystemAdministrator.LastBackups.Application.BackupsGetLastNDays
{
  public class BackupsGetAllLastNDaysQuery(int days) : Query
  {
    public int Days { get; } = days;
  }
}