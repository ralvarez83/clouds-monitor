using Shared.Domain.ValueObjects;

namespace Shared.Domain.ValueObjects
{
  public class BackupType : ValueObjectsBase
  {
    private readonly string? _value = null;


    private BackupType(string value)
    {
      _value = value ?? throw new ArgumentNullException("value");
    }

    private const string VirtualMachineValue = "VirtualMachine";

    public static BackupType VirtualMachine { get; } = new BackupType(VirtualMachineValue);

    public static BackupType Parse(string value)
    {
      Dictionary<string, BackupType?> parser = new Dictionary<string, BackupType?>(){
        {VirtualMachineValue, VirtualMachine}
      };

      BackupType? type = parser.GetValueOrDefault(value);

      return type ?? throw new ArgumentNullException("value");
    }
    public override string ToString()
    {
      return _value ?? string.Empty;
    }

    public BackupType Copy()
    {
      return new BackupType(_value);
    }

    public static string GetName() => "BackupType";
  }
}