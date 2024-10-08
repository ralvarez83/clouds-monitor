using Clouds.LastBackups.Infrastructure.Repository.MongoDB;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using Shared.Infrastructure.MongoDB;

namespace CloudsTest.LastBackups.Infrastructure.MongoDB
{
  public abstract class CloudMongoDBUnitCase : MongoDBUnitCase<LastBackupsStatusContext>
  {

    protected override LastBackupsStatusContext GetTemporalDBContext()
    {
      var client = new MongoClient(mongoDBSettings.MongoDBURI);
      return Create(client.GetDatabase(dataBaseName)); ;
    }

    private LastBackupsStatusContext Create(IMongoDatabase database)
    {
      return new(new DbContextOptionsBuilder<LastBackupsStatusContext>()
          .UseMongoDB(database.Client, database.DatabaseNamespace.DatabaseName)
          .Options);
    }
  }
}