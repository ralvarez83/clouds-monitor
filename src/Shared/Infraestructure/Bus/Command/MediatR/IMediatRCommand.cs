using MediatR;

namespace Shared.Shared.Infraestructure.Bus.Command.MediatR
{
  public interface IMediatRCommand<TRequest> : IRequest
  {
    public abstract static IRequest Wrapper(TRequest request);
  }
}