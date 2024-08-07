namespace SystemAdministrator.LastBackups.Domain.ValueObjects;

public class BackupName(string value)
{
  public string Value { get; private set; } = value;
}