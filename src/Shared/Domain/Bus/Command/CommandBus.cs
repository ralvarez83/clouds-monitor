namespace Shared.Domain.Bus.Command
{
    public interface CommandBus
    {
        Task Dispatch(Command command);

    }
}