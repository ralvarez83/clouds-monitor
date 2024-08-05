using MediatR;
using CommandDomain = Shared.Domain.Bus.Command.Command;

namespace Shared.Infraestructure.Bus.MediatR.Command
{
  public interface IMediatRCommandDirectoryWrapper
  {
    public Dictionary<Type, Func<CommandDomain, IBaseRequest>> GetWrappers();
  }
}