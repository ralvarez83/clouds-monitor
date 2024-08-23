using MediatR;
using Shared.Domain.Bus.Query;
using QueryDomain = Shared.Domain.Bus.Query.Query;

namespace Shared.Infrastructure.Bus.Query.MediatR
{
    public class MediatRQueryBus(Mediator mediator, IMediatRQueryDirectoryWrapper queryDirectoryWrapper) : QueryBus
    {
        private readonly Mediator _mediator = mediator;
        private IMediatRQueryDirectoryWrapper _typeOfQueries = queryDirectoryWrapper;


        public Task<TResponse> Ask<TResponse>(QueryDomain request)
        {
            IRequest<TResponse> query = (IRequest<TResponse>)TransformQuery(request);
            return _mediator.Send<TResponse>(query);
        }

        private QueryDomain TransformQuery(QueryDomain request)
        {
            Func<QueryDomain, IBaseRequest>? wrapper = _typeOfQueries.GetWrappers().GetValueOrDefault(request.GetType());
            if (null != wrapper)
            {
                return (QueryDomain)wrapper(request);
            }

            return request;
        }
    }
}