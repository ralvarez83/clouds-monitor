using Clouds.LastBackups.Infraestructure.Repository.MongoDB;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using SharedTest.Infrastructure;

namespace CloudsTest.LastBackups.Infrastructure.MongoDB
{
  public class MongoDBUnitCase : InfrastructureTestCase, IDisposable
  {
    private MongoDBSettings mongoDBSettings;

    private string dataBaseName;

    public MongoDBUnitCase()
    {
      dataBaseName = mongoDBSettings.DatabaseName + DateTime.Now.Ticks;
    }

    protected void DropDataBase()
    {
      var client = new MongoClient(mongoDBSettings.MongoDBURI);
      client.DropDatabase(dataBaseName);
    }

    protected LastBackupsStatusContext GetTemporalDBContext()
    {
      var client = new MongoClient(mongoDBSettings.MongoDBURI);
      return LastBackupsStatusContext.Create(client.GetDatabase(dataBaseName)); ;
    }

    protected override Action<IServiceCollection> GetServices()
    {
      return services => { };
    }

    protected override void Setup()
    {
      MongoDBSettings? mongoSettings = GetSection<MongoDBSettings>(MongoDBSettings.Name);
      if (null == mongoSettings)
        throw new Exception("Section MongoDBSettings not found");
      mongoDBSettings = mongoSettings;
    }

    public void Dispose()
    {
      Finish();
    }
  }
}