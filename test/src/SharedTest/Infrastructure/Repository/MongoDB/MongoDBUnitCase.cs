using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Shared.Infrastructure.Repository.MongoDB;
using SharedTest.Infrastructure;

namespace Shared.Infrastructure.MongoDB
{
  public abstract class MongoDBUnitCase<TContext> : InfrastructureTestCase, IDisposable where TContext : DbContext
  {
    protected MongoDBSettings mongoDBSettings;

    protected string dataBaseName;

    public MongoDBUnitCase() : base()
    {
      dataBaseName = null != mongoDBSettings ? mongoDBSettings.DatabaseName + DateTime.Now.Ticks : string.Empty;
    }

    protected void DropDataBase()
    {
      var client = new MongoClient(mongoDBSettings.MongoDBURI);
      client.DropDatabase(dataBaseName);
    }

    protected abstract TContext GetTemporalDBContext();

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