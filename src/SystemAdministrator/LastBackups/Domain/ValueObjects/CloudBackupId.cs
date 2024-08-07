namespace SystemAdministrator.LastBackups.Domain.ValueObjects;

public class CloudBackupId(string value)
{
  public string Value { get; private set; } = value;

}