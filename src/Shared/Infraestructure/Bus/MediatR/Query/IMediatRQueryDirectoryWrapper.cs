using MediatR;
using QueryDomain = Shared.Domain.Bus.Query.Query;

namespace Shared.Infraestructure.Bus.MediatR.Query
{
  public interface IMediatRQueryDirectoryWrapper
  {
    public Dictionary<Type, Func<QueryDomain, IBaseRequest>> GetWrappers();
  }
}