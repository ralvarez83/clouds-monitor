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

        protected string? GetConnectionString(string connectionStringName)
        {
            return configuration.GetConnectionString(connectionStringName);
        }

        protected abstract Action<IServiceCollection> GetServices();

        protected void Eventually(Action function)
        {
            var attempts = 0;
            var allOk = false;
            while (attempts < MaxAttempts && !allOk)
                try
                {
                    function.Invoke();
                    allOk = true;
                }
                catch (Exception e)
                {
                    attempts++;

                    if (attempts > MaxAttempts)
                        throw new Exception($"Could not assert after some retries. Last error: {e.Message}");

                    Thread.Sleep(MillisToWaitBetweenRetries);
                }
        }

        protected async Task WaitFor(Func<Task<bool>> function)
        {
            var attempts = 0;
            var allOk = false;
            while (attempts < MaxAttempts && !allOk)
                try
                {
                    allOk = await function.Invoke();
                    if (!allOk) throw new Exception();
                }
                catch (Exception e)
                {
                    attempts++;

                    if (attempts > MaxAttempts)
                        throw new Exception($"Could not assert after some retries. Last error: {e.Message}");

                    Thread.Sleep(MillisToWaitBetweenRetries);
                }
        }
    }
}
