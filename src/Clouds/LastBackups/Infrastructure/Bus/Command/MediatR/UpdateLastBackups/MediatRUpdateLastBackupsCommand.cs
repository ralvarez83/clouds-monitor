using MediatR;
using Shared.Shared.Infraestructure.Bus.Command.MediatR;
using Clouds.LastBackups.Application.UpdateLastBackups;
using CommandDomain = Shared.Domain.Bus.Command.Command;

namespace Clouds.LastBackups.Infrastructure.Bus.Command.MediatR.UpdateLastBackups
{
  public class MediatRUpdateLastBackupsCommand : UpdateLastBackupsCommand, IMediatRCommand<CommandDomain>
  {
    public static IRequest Wrapper(CommandDomain request) => new MediatRUpdateLastBackupsCommand();
  }
}