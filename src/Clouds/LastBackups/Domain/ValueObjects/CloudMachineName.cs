using Shared.Domain.ValueObjects;

namespace Clouds.LastBackups.Domain.ValueObjects;

public class CloudMachineName(string value) : ValueObjectsBase
{
  public string Value { get; private set; } = value;

  public static string GetName() => "CloudMachineName";
}