namespace Clouds.LastBackups.Infraestructure.Repository.MongoDB
{
  public record MongoDBSettings(string MongoDBURI, string DatabaseName)
  {
    public const string Name = "MongoDBSettings";
  }

}