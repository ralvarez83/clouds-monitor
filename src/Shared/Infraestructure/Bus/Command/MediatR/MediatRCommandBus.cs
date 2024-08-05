using MediatR;
using Shared.Domain.Bus.Command;
using CommandDomain = Shared.Domain.Bus.Command.Command;

namespace Shared.Infraestructure.Bus.Command.MediatR
{
    public class MediatRCommandBus(Mediator mediator, IMediatRCommandDirectoryWrapper commandDirectoryWrapper) : CommandBus
    {
        private readonly Mediator _mediator = mediator;
        private IMediatRCommandDirectoryWrapper _typeOfCommands = commandDirectoryWrapper;


        public Task<TResponse> Ask<TResponse>(CommandDomain request)  //////////
        {
            return _mediator.Send((IRequest<TResponse>)TransformQuery(request));
        }

        public Task Dispatch(CommandDomain command)
        {
            throw new NotImplementedException();
        }

        private CommandDomain TransformQuery(CommandDomain request)
        {
            Func<CommandDomain, IBaseRequest>? wrapper = _typeOfCommands.GetWrappers().GetValueOrDefault(request.GetType());
            if (null != wrapper)
            {
                return (CommandDomain)wrapper(request);
            }

            return request;
        }
    }
}