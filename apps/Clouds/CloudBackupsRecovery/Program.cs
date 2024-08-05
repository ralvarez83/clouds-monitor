using Microsoft.Extensions.Hosting;
using CloudBackupsRecovery.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using CloudBackupsRecovery;

//var value = System.Environment.GetEnvironmentVariable("AZURE_CLIENT_ID");

HostApplicationBuilder hostBuilder = Host.CreateApplicationBuilder(args);

hostBuilder.Services.AddApplication();
hostBuilder.Services.AddConfiguration(hostBuilder.Configuration);
hostBuilder.Services.AddInfraestructure();
hostBuilder.Services.AddHostedService<ApplicationHostedService>();

IHost host = hostBuilder.Build();

await host.RunAsync();
