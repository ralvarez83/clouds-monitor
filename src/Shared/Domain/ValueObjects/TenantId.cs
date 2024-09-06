namespace Shared.Domain.ValueObjects
{
  public class TenantId(string value) : ValueObjectsBase
  {
    public string Value { get; } = value;

    public static string GetName() => "TenantId";
  }
}