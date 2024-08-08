namespace Shared.Infrastructure.Enviroment
{
  public record EnvironmentVariables(string Key, string Value)
  {
    public const string Name = "EnvironmentVariables";
  }
}