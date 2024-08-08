using MediatR;

namespace Shared.Shared.Infraestructure.Bus.Query.MediatR
{
  public interface IMediatRQuery<out TResponse, TRequest> : IRequest<TResponse>
  {
    public abstract static IRequest<TResponse> Wrapper(TRequest request);
  }
}