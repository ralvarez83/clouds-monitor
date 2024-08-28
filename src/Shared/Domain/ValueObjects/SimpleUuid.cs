namespace Shared.Domain.ValueObjects;

public class SimpleUuid : ValueObjectsBase
{
    public Guid Value { get; init; }

    public SimpleUuid(Guid guid)
    {
        Value = guid;
    }

    public SimpleUuid()
    {
        Value = Guid.NewGuid();
    }

    public override string ToString()
    {
        return Value.ToString();
    }

    public static string GetName() => "SimpleUuid";
}