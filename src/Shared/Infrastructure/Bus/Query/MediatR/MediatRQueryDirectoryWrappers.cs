using System.Linq.Expressions;
using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using QueryDomainInterface = Shared.Domain.Bus.Query.Query;

namespace Shared.Infrastructure.Bus.Query.MediatR
{
  public class MediatRQueryDirectoryWrapper(IServiceProvider serviceProvider) : IMediatRQueryDirectoryWrapper
  {
    private static Dictionary<Type, Func<QueryDomainInterface, IBaseRequest>> queryWrappers;
    private readonly IServiceProvider serviceProvider = serviceProvider;

    public Dictionary<Type, Func<QueryDomainInterface, IBaseRequest>> GetWrappers()
    {
      if (null == queryWrappers)
      {
        queryWrappers = GetDictionary(DiscoverWrappers());
      }
      return queryWrappers;
    }

    private Dictionary<Type, Func<QueryDomainInterface, IBaseRequest>> GetDictionary(List<Type> wrappersTypes)
    {
      Dictionary<Type, Func<QueryDomainInterface, IBaseRequest>> queryWrapperDictionary = [];
      foreach (Type wrapperType in wrappersTypes)
      {
        Type queryType = wrapperType.BaseType!;
        var instance = Activator.CreateInstance(wrapperType);
        MethodInfo methodInfo = wrapperType.GetMethod("Wrapper")!;
        var delegateMethod = methodInfo.CreateDelegate<Func<QueryDomainInterface, IBaseRequest>>();

        queryWrapperDictionary.Add(queryType, delegateMethod);
      }
      return queryWrapperDictionary;
    }

    private List<Type> DiscoverWrappers()
    {

      Type wrapperQueryType = typeof(IMediatRQuery<,>);
      IServiceScope scope = serviceProvider.CreateScope();

      // Type[] shared = AppDomain.CurrentDomain.GetAssemblies()
      //  .Where(assembles => assembles.FullName.Contains("Shared"))
      //  .SelectMany(assembles => assembles.GetTypes()).ToArray<Type>();

      List<Type> wrappersType = AppDomain.CurrentDomain.GetAssemblies()
      .SelectMany(assembles => assembles.GetTypes())
      .Where(type => null != type.GetInterface(wrapperQueryType.Name) && !type.IsAbstract).ToList();
      // .Where(type => null != type.BaseType && type.BaseType.Name.Equals(subscriberType.Name) && !type.IsAbstract).ToList();

      return wrappersType;
    }

  }
}