using Microsoft.Extensions.Hosting;
using CloudBackupsRecovery.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Shared.Infrastructure.Enviroment;
using RegisterBackupsConsumer;

HostApplicationBuilder hostBuilder = Host.CreateApplicationBuilder(args);

hostBuilder.Services.AddApplication();
hostBuilder.Services.AddConfiguration(hostBuilder.Configuration);
hostBuilder.Services.AddInfraestructure(hostBuilder.Configuration);
hostBuilder.Services.AddHostedService<ApplicationHostedService>();

IHost host = hostBuilder.Build();

LoadEnvironmentVariables? loadEnvironmentVariables = host.Services.GetService<LoadEnvironmentVariables>();

if (null == loadEnvironmentVariables)
  throw new Exception("LoadEnvironmentVariables not found");

loadEnvironmentVariables.RegisterVariablesIfNotExists();

await host.RunAsync();
