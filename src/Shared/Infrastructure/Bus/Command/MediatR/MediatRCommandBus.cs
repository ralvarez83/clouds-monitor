using MediatR;
using Shared.Domain.Bus.Command;
using CommandDomain = Shared.Domain.Bus.Command.Command;

namespace Shared.Infrastructure.Bus.Command.MediatR
{
    public class MediatRCommandBus(Mediator mediator, IMediatRCommandDirectoryWrapper commandDirectoryWrapper) : CommandBus
    {
        private readonly Mediator _mediator = mediator;
        private IMediatRCommandDirectoryWrapper _typeOfCommands = commandDirectoryWrapper;


        public Task Dispatch(CommandDomain command)
        {
            return _mediator.Send((IRequest)TransformCommand(command));
        }

        private CommandDomain TransformCommand(CommandDomain request)
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