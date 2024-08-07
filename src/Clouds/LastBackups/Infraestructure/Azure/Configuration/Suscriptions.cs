namespace Clouds.LastBackups.Infraestructure.Azure.Configuration;

// public record Suscriptions (string Id, List<ResourceGroups> ResourceGroups)
public class Suscriptions
{

  public const string Name = "Suscriptions";
  public string Id { get; set; } = string.Empty;
  public List<ResourcesGroups> ResourcesGroups { get; set; } = [];
}
