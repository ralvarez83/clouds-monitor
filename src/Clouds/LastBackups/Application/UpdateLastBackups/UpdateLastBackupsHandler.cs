using Shared.Domain.Bus.Command;

namespace Clouds.LastBackups.Application.UpdateLastBackups
{
  public class UpdateLastBackupsHandler(UpdateLastBackups updateLastBackups) : CommandHandler<UpdateLastBackupsCommand>
  {
    private UpdateLastBackups _updateLastBackups { get; } = updateLastBackups;

    public async Task Handle(UpdateLastBackupsCommand command)
    {
      await _updateLastBackups.Run();
    }
  }
}