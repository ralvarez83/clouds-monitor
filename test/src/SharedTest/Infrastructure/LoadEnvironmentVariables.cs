using Microsoft.Extensions.Options;
using SharedTest.Infrastructure;

namespace SharedTest.Infrastructure
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
  }
}