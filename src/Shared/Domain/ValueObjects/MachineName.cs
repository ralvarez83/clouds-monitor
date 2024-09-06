namespace Shared.Domain.ValueObjects;

public class MachineName(string value) : ValueObjectsBase
{
  public string Value { get; private set; } = value;

  public static string GetName() => "MachineName";
}