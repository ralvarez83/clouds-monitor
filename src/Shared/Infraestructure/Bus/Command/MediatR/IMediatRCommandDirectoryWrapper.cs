using MediatR;
using CommandDomain = Shared.Domain.Bus.Command.Command;

namespace Shared.Infraestructure.Bus.Command.MediatR
{
  public interface IMediatRCommandDirectoryWrapper
  {
    public Dictionary<Type, Func<CommandDomain, IBaseRequest>> GetWrappers();
  }
}