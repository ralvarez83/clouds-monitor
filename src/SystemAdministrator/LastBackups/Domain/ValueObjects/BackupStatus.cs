namespace SystemAdministrator.LastBackups.Domain.ValueObjects;

public class BackupStatus(string value)
{
  public string Value { get; private set; } = value;
}