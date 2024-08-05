using MediatR;

namespace Shared.Shared.Infraestructure.Bus.Common.MediatR
{
  public interface IMediatRResponse<out TResponse, TRequest> : IRequest<TResponse>
  {
    public abstract static IRequest<TResponse> Wrapper(TRequest request);
  }
}