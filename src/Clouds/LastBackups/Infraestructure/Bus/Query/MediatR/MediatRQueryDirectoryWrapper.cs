using Clouds.LastBackups.Application.GetCloudLast;
using Clouds.LastBackups.Infraestructure.Bus.MediatR.GetCloudLast;
using MediatR;
using Shared.Infrastructure.Bus.Query.MediatR;

namespace Clouds.LastBackups.Infraestructure.Bus.Query.MediatR
{
  public class MediatRQueryDirectoryWrapper : IMediatRQueryDirectoryWrapper
  {
    private Dictionary<Type, Func<Shared.Domain.Bus.Query.Query, IBaseRequest>> queryWrappers = new Dictionary<Type, Func<Shared.Domain.Bus.Query.Query, IBaseRequest>>{
      {typeof(GetCloudLastBackupsQuery), MediatRGetCloudLastBackupsQuery.Wrapper}
    };

    public Dictionary<Type, Func<Shared.Domain.Bus.Query.Query, IBaseRequest>> GetWrappers()
    {
      return queryWrappers;
    }
  }
}