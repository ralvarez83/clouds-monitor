using Clouds.LastBackups.Infraestructure.Repository.MongoDB;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Shared.Infraestructure.Repository.MongoDB;
using Shared.Infrastructure.MongoDB;
using SharedTest.Infrastructure;

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