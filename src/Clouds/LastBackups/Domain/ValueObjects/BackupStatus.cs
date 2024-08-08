using Shared.Domain.ValueObjects;

namespace Clouds.LastBackups.Domain.ValueObjects;

public class BackupStatus : ValueObjectsBase
{
  private readonly string? _value = null;


  private BackupStatus(string value)
  {
    _value = value ?? throw new ArgumentNullException("value");
  }

  private const string CompletedValue = "Completed";
  private const string IncompletedValue = "Incompleted";

  public static BackupStatus Completed { get; } = new BackupStatus(CompletedValue);
  public static BackupStatus Incompleted { get; } = new BackupStatus(IncompletedValue);

  public static BackupStatus Parse(string value)
  {
    Dictionary<string, BackupStatus?> parser = new(){
      {CompletedValue, Completed},
      {IncompletedValue, Incompleted}
    };

    BackupStatus? status = parser.GetValueOrDefault(value);

    return status ?? throw new ArgumentNullException("value");
  }

  public override string ToString()
  {
    return _value ?? string.Empty;
  }

  public static string GetName() => "BackupStatus";
}