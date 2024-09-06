namespace Shared.Domain.ValueObjects;

public class BackupDate : SimpleDate
{
  public BackupDate(DateTime value) : base(value)
  {
  }

  public BackupDate(DateTimeOffset value) : base(value)
  {
  }
  public BackupDate(string date) : base(date)
  {
  }

  public static new string GetName() => "BackupDate";

}