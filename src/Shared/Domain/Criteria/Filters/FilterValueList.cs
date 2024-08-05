namespace Shared.Domain.Criteria.Filters
{
  public class FilterValueList
  {
    public List<string> Value { get; } = [];
    public const string SEPARATOR = ",";

    public FilterValueList()
    {

    }

    public FilterValueList(string listOfValues)
    {
      Value = listOfValues.Split(SEPARATOR).ToList();
    }

    public void AddValue(string newValue)
    {
      Value.Add(newValue);
    }

    public void AddRange(List<string> newValues)
    {
      Value.AddRange(newValues);
    }
    public override string ToString()
    {
      return string.Join(",", Value);
    }
  }
}