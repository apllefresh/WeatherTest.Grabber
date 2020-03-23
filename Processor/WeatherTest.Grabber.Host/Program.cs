using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Threading;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace WeatherTest.Grabber.Host
{
    internal static class Program
    {
        private static async Task Main()
        {
            var cancellationToken = new CancellationToken();

            var builder = Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder()
                .UseWindowsService()
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddJsonFile("appsettings.json", optional: true);
                })
                .ConfigureServices(Startup.ConfigureServices)
                .ConfigureLogging((hostingContext, logging) =>
                {
                    logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                    logging.AddConsole();
                }).Build();

            await builder.RunAsync(cancellationToken);
        }
    }
}