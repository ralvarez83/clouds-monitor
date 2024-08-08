using Clouds.LastBackups.Infraestructure.Bus.Command.MediatR.UpdateLastBackups;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Shared.Domain.Bus.Command;

namespace CloudBackupsRecovery
{
    public class ApplicationHostedService : IHostedService, IHostedLifecycleService
    {

        private readonly ILogger _logger;
        private readonly CommandBus _commandBus;

        public ApplicationHostedService(
            ILogger<ApplicationHostedService> logger,
            IHostApplicationLifetime appLifetime, CommandBus commandBus)
        {
            _logger = logger;
            _commandBus = commandBus;

            appLifetime.ApplicationStarted.Register(OnStarted);
            appLifetime.ApplicationStopping.Register(OnStopping);
            appLifetime.ApplicationStopped.Register(OnStopped);
        }

        private void OnStarted()
        {
            _commandBus.Dispatch(new MediatRUpdateLastBackupsCommand());
        }

        Task IHostedLifecycleService.StartingAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("1. StartingAsync has been called.");

            return Task.CompletedTask;
        }

        Task IHostedService.StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("2. StartAsync has been called.");

            return Task.CompletedTask;
        }

        Task IHostedLifecycleService.StartedAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("3. StartedAsync has been called.");

            return Task.CompletedTask;
        }


        private void OnStopping()
        {
            _logger.LogInformation("5. OnStopping has been called.");
        }

        Task IHostedLifecycleService.StoppingAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("6. StoppingAsync has been called.");

            return Task.CompletedTask;
        }

        Task IHostedService.StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("7. StopAsync has been called.");

            return Task.CompletedTask;
        }

        Task IHostedLifecycleService.StoppedAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("8. StoppedAsync has been called.");

            return Task.CompletedTask;
        }

        private void OnStopped()
        {
            _logger.LogInformation("9. OnStopped has been called.");
        }
    }
}