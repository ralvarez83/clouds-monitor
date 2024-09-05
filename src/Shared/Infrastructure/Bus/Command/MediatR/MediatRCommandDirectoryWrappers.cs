using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Shared.Shared.Infraestructure.Bus.Command.MediatR;
using CommandDomainInterface = Shared.Domain.Bus.Command.Command;

namespace Shared.Infrastructure.Bus.Command.MediatR
{
  public class MediatRCommandDirectoryWrapper(IServiceProvider serviceProvider) : IMediatRCommandDirectoryWrapper
  {
    private static Dictionary<Type, Func<CommandDomainInterface, IBaseRequest>> commandWrappers;
    private readonly IServiceProvider serviceProvider = serviceProvider;

    public Dictionary<Type, Func<CommandDomainInterface, IBaseRequest>> GetWrappers()
    {
      if (null == commandWrappers)
      {
        commandWrappers = GetDictionary(DiscoverWrappers());
      }
      return commandWrappers;
    }

    private Dictionary<Type, Func<CommandDomainInterface, IBaseRequest>> GetDictionary(List<Type> wrappersTypes)
    {
      Dictionary<Type, Func<CommandDomainInterface, IBaseRequest>> commandWrapperDictionary = [];
      foreach (Type wrapperType in wrappersTypes)
      {
        Type commandType = wrapperType.BaseType!;
        var instance = Activator.CreateInstance(wrapperType);
        MethodInfo methodInfo = wrapperType.GetMethod("Wrapper")!;
        var delegateMethod = methodInfo.CreateDelegate<Func<CommandDomainInterface, IBaseRequest>>();

        commandWrapperDictionary.Add(commandType, delegateMethod);
      }
      return commandWrapperDictionary;
    }

    private List<Type> DiscoverWrappers()
    {

      Type wrapperCommandType = typeof(IMediatRCommand<>);
      IServiceScope scope = serviceProvider.CreateScope();

      // Type[] shared = AppDomain.CurrentDomain.GetAssemblies()
      //  .Where(assembles => assembles.FullName.Contains("Shared"))
      //  .SelectMany(assembles => assembles.GetTypes()).ToArray<Type>();

      List<Type> wrappersType = AppDomain.CurrentDomain.GetAssemblies()
      .SelectMany(assembles => assembles.GetTypes())
      .Where(type => null != type.GetInterface(wrapperCommandType.Name) && !type.IsAbstract).ToList();

      return wrappersType;
    }

  }
}