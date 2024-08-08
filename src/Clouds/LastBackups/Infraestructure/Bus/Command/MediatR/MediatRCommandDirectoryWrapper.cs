using Clouds.LastBackups.Application.UpdateLastBackups;
using Clouds.LastBackups.Infraestructure.Bus.Command.MediatR.UpdateLastBackups;
using MediatR;
using Shared.Infraestructure.Bus.Command.MediatR;

namespace Clouds.LastBackups.Infraestructure.Bus.Command.MediatR
{
  public class MediatRCommandDirectoryWrapper : IMediatRCommandDirectoryWrapper
  {
    private Dictionary<Type, Func<Shared.Domain.Bus.Command.Command, IBaseRequest>> commandWrappers = new Dictionary<Type, Func<Shared.Domain.Bus.Command.Command, IBaseRequest>>{
      {typeof(UpdateLastBackupsCommand), MediatRUpdateLastBackupsCommand.Wrapper}
    };
    public Dictionary<Type, Func<Shared.Domain.Bus.Command.Command, IBaseRequest>> GetWrappers()
    {

      return commandWrappers;
    }
  }
}