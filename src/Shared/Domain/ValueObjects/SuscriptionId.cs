using Shared.Domain.ValueObjects;

namespace Shared.Domain.ValueObjects
{
  public class SuscriptionId(string value) : ValueObjectsBase
  {
    public string Value { get; } = value;

    public static string GetName() => "SuscriptionId";
  }
}