using Shared.Domain.ValueObjects;

namespace Clouds.LastBackups.Domain.ValueObjects
{
  public class VaultId(string value) : ValueObjectsBase
  {
    public string Value { get; } = value;

    public static string GetName() => "VaultId";
  }
}