namespace Shared.Domain.Bus.Query;

public interface QueryBus
{
    public Task<TResponse> Ask<TResponse>(Query request);
}