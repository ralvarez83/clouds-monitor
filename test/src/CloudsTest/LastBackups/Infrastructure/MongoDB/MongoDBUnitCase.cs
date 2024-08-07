using Clouds.LastBackups.Infraestructure.Repository.MongoDB;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using SharedTest.Infrastructure;

namespace CloudsTest.LastBackups.Infrastructure.MongoDB
{
  public class MongoDBUnitCase : InfrastructureTestCase, IDisposable
  {
    private string? mongoDBConnectionString;
    private const string DATA_BASE_NAME_PREFIX = "MongoDBUnitCase";

    private string dataBaseName;

    public MongoDBUnitCase()
    {
      dataBaseName = DATA_BASE_NAME_PREFIX + DateTime.Now.Ticks;
    }

    protected void DropDataBase()
    {
      var client = new MongoClient(mongoDBConnectionString);
      client.DropDatabase(dataBaseName);
    }

    protected LastBackupsStatusContext GetTemporalDBContext()
    {
      var client = new MongoClient(mongoDBConnectionString);
      return LastBackupsStatusContext.Create(client.GetDatabase(dataBaseName)); ;
    }

    protected override Action<IServiceCollection> GetServices()
    {
      return services => { };
    }

    protected override void Setup()
    {
      mongoDBConnectionString = GetConnectionString("mongoDB");
    }

    public void Dispose()
    {
      Finish();
    }
  }
}