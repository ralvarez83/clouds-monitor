using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace SharedTest.Infrastructure
{
    public abstract class InfrastructureTestCase
    {
        private const int MaxAttempts = 5;
        private const int MillisToWaitBetweenRetries = 300;
        private readonly IHost host;
        private readonly IConfiguration configuration;

        public InfrastructureTestCase()
        {
            configuration = Configuration();
            host = CreateHost();
            Setup();
        }

        protected abstract void Setup();

        protected void Finish()
        {
            host.Dispose();
        }

        protected IHost CreateHost()
        {
            var hostBuilder = new HostBuilder()
                 .ConfigureWebHostDefaults(webHost =>
                 {
                     webHost.UseTestServer();
                     //  webHost.UseStartup<Worker>();
                     webHost.ConfigureTestServices(GetServices());
                     webHost.UseConfiguration(Configuration());
                 });
            IHost host = hostBuilder.Build();
            return host;
        }

        private static IConfigurationRoot Configuration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(AppContext.BaseDirectory))
                .AddJsonFile("appsettings.json", true, true);

            return builder.Build();
        }

        protected T? GetService<T>()
        {
            return host.Services.GetService<T>();
        }

        protected T? GetSection<T>(string key) => configuration.GetSection(key).Get<T>();

        protected string? GetConnectionString(string connectionStringName)
        {
            return configuration.GetConnectionString(connectionStringName);
        }

        protected abstract Action<IServiceCollection> GetServices();
    }
}
