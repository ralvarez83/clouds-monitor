using Shared.Domain.ValueObjects;

namespace Shared.Domain.ValueObjects;

public class MachineId(string value) : ValueObjectsBase
{
  public string Value { get; private set; } = value;

  public static string GetName() => "MachineId";
}