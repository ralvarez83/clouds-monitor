namespace SharedTest.Infrastructure
{
  public record EnvironmentVariables(string Key, string Value)
  {
    public const string NAME = "EnvironmentVariables";
  }
}