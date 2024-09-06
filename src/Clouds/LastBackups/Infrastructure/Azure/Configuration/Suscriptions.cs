namespace Clouds.LastBackups.Infrastructure.Azure.Configuration;
public class Suscriptions
{

  public const string Name = "Suscriptions";
  public string Id { get; set; } = string.Empty;
  public List<ResourcesGroups> ResourcesGroups { get; set; } = [];
}
