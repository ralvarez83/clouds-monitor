using Microsoft.Extensions.Options;

namespace Shared.Infrastructure.Enviroment
{
  public class LoadEnvironmentVariables(IOptions<List<EnvironmentVariables>> variables)
  {
    private readonly List<EnvironmentVariables> variables = variables.Value;

    public void RegisterVariables()
    {
      foreach (EnvironmentVariables variable in variables)
      {
        Environment.SetEnvironmentVariable(variable.Key, variable.Value);
      }
    }
    public void RegisterVariablesIfNotExists()
    {
      foreach (EnvironmentVariables variable in variables)
      {
        string? environmentVariable = Environment.GetEnvironmentVariable(variable.Key);
        if (null == environmentVariable)
          Environment.SetEnvironmentVariable(variable.Key, variable.Value);
      }
    }
  }
}