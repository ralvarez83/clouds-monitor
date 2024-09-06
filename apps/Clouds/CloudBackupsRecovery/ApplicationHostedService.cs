using Clouds.LastBackups.Application.UpdateLastBackups;
using Clouds.LastBackups.Domain;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Shared.Domain.Bus.Command;
using Shared.Domain.Bus.Event;
using Shared.Domain.Bus.Query;

namespace CloudBackupsRecovery
{
    public class ApplicationHostedService : IHostedService, IHostedLifecycleService
    {

        private readonly ILogger _logger;
        private readonly CommandBus _commandBus;
        private readonly QueryBus queryBus;
        private readonly LastBackupsRepository repository;
        private readonly EventBus eventBus;

        public ApplicationHostedService(
            ILogger<ApplicationHostedService> logger,
            IHostApplicationLifetime appLifetime,
            CommandBus commandBus,
            QueryBus queryBus,
            LastBackupsRepository repository,
            EventBus eventBus)
        {
            _logger = logger;
            _commandBus = commandBus;
            this.queryBus = queryBus;
            this.repository = repository;
            this.eventBus = eventBus;
            appLifetime.ApplicationStarted.Register(OnStarted);
            appLifetime.ApplicationStopping.Register(OnStopping);
            appLifetime.ApplicationStopped.Register(OnStopped);
        }

        private void OnStarted()
        {

        }

        Task IHostedLifecycleService.StartingAsync(CancellationToken cancellationToken)
        {
            // _logger.LogInformation("1. StartingAsync has been called.");

            return Task.CompletedTask;
        }

        async Task IHostedService.StartAsync(CancellationToken cancellationToken)
        {
            // _logger.LogInformation("2. StartAsync has been called.");

            // Las 2 siguientes líneas son necesarias para detectar excepciones.
            // UpdateLastBackupsHandler handler = new UpdateLastBackupsHandler(new UpdateLastBackups(repository, queryBus, eventBus));
            // await handler.Handle(new UpdateLastBackupsCommand());

            _logger.LogInformation("Cloud data update: RUNNING");
            await _commandBus.Dispatch(new UpdateLastBackupsCommand());
            _logger.LogInformation("Cloud data update: DONE");
            return;
        }

        Task IHostedLifecycleService.StartedAsync(CancellationToken cancellationToken)
        {
            // _logger.LogInformation("3. StartedAsync has been called.");


            return Task.CompletedTask;
        }


        private void OnStopping()
        {
            // _logger.LogInformation("5. OnStopping has been called.");
        }

        Task IHostedLifecycleService.StoppingAsync(CancellationToken cancellationToken)
        {
            // _logger.LogInformation("6. StoppingAsync has been called.");

            return Task.CompletedTask;
        }

        Task IHostedService.StopAsync(CancellationToken cancellationToken)
        {
            // _logger.LogInformation("7. StopAsync has been called.");

            return Task.CompletedTask;
        }

        Task IHostedLifecycleService.StoppedAsync(CancellationToken cancellationToken)
        {
            // _logger.LogInformation("8. StoppedAsync has been called.");

            return Task.CompletedTask;
        }

        private void OnStopped()
        {
            // _logger.LogInformation("9. OnStopped has been called.");
        }
    }
}