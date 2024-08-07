using Shared.Domain.ValueObjects;

namespace SystemAdministrator.LastBackups.Domain.ValueObjects;

public class BackupDate(DateTime value) : ValueObjectsBase
{
  public DateTime Value { get; private set; } = value;

  public static string GetName() => "BackupDate";
}