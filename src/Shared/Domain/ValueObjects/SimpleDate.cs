using System.Globalization;

namespace Shared.Domain.ValueObjects
{
  public class SimpleDate : ValueObjectsBase
  {
    public DateTime Value { get; private set; }

    public SimpleDate(DateTime value)
    {
      Value = value.ToUniversalTime();
    }

    public SimpleDate(DateTimeOffset value)
    {
      Value = value.UtcDateTime;
    }
    public SimpleDate(string date)
    {
      Value = DateTime.ParseExact(date, "s", CultureInfo.CurrentCulture);
    }

    public static SimpleDate Now()
    {
      return new SimpleDate(DateTime.Now);
    }

    public override string ToString()
    {
      return Value.ToString("s", CultureInfo.CurrentCulture);
    }

    public static string GetName() => "SimpleDate";
  }
}