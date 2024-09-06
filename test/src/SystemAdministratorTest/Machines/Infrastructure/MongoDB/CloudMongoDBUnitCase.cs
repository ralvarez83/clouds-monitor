using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using Shared.Infrastructure.MongoDB;
using SystemAdministrator.Machines.Infrastructure.Repository.MongoDB;

namespace SystemAdministrationTest.Machines.Infrastructure.MongoDB
{
  public abstract class CloudMongoDBUnitCase : MongoDBUnitCase<MachinesContext>
  {

    protected override MachinesContext GetTemporalDBContext()
    {
      var client = new MongoClient(mongoDBSettings.MongoDBURI);
      return Create(client.GetDatabase(dataBaseName)); ;
    }

    private MachinesContext Create(IMongoDatabase database)
    {
      return new(new DbContextOptionsBuilder<MachinesContext>()
          .UseMongoDB(database.Client, database.DatabaseNamespace.DatabaseName)
          .Options);
    }
  }
}