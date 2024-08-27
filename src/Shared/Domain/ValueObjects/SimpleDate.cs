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
      Value = DateTime.ParseExact(date, "s", CultureInfo.CurrentCulture).ToUniversalTime();
    }

    public static SimpleDate Now()
    {
      return new SimpleDate(DateTime.Now);
    }

    public override string ToString()
    {
      return Value.ToString("s", CultureInfo.CurrentCulture);
    }

    public override bool Equals(object? obj)
    {
      if (null == obj || obj.GetType() != this.GetType())
        return false;

      SimpleDate date = (SimpleDate)obj;

      return this.Value.Day == date.Value.Day
              && this.Value.Month == date.Value.Month
              && this.Value.Year == date.Value.Year
              && this.Value.Second == date.Value.Second
              && this.Value.Minute == date.Value.Minute
              && this.Value.Hour == date.Value.Hour;
    }

    public static string GetName() => "SimpleDate";
  }
}