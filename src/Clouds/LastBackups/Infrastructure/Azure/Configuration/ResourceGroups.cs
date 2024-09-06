namespace Clouds.LastBackups.Infrastructure.Azure.Configuration;

public record ResourcesGroups
{
  public string Name { get; set; } = string.Empty;
  public Vaults[] Vaults { get; set; } = [];
}
