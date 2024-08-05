using MediatR;

namespace Shared.Shared.Infraestructure.Bus.MediatR
{
  public interface IMediatRResponse<out TResponse, TRequest>  : IRequest<TResponse>
  {
    public abstract static IRequest<TResponse> Wrapper (TRequest request);
  }
}