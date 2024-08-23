using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using Shared.Infrastructure.MongoDB;
using SystemAdministrator.LastBackups.Infrastructure.Repository.MongoDB;

namespace SystemAdministrationTest.LastBackups.Infrastructure.MongoDB
{
  public abstract class CloudMongoDBUnitCase : MongoDBUnitCase<BackupsContext>
  {

    protected override BackupsContext GetTemporalDBContext()
    {
      var client = new MongoClient(mongoDBSettings.MongoDBURI);
      return Create(client.GetDatabase(dataBaseName)); ;
    }

    private BackupsContext Create(IMongoDatabase database)
    {
      return new(new DbContextOptionsBuilder<BackupsContext>()
          .UseMongoDB(database.Client, database.DatabaseNamespace.DatabaseName)
          .Options);
    }
  }
}