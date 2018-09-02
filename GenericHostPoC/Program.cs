using GenericHostPoC.ExternalServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MyApplication.Services;
using MyApplication.Services.External;
using System.IO;
using System.Threading.Tasks;

namespace GenericHostPoC
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await new HostBuilder()
                .ConfigureHostConfiguration(configHost => ConfigureHost(configHost, args))
                .ConfigureAppConfiguration((hostContext, configApp) => ConfigureApp(hostContext, configApp, args))
                .ConfigureServices(ConfigureServices)
                .ConfigureLogging(ConfigureLogging)
                .UseConsoleLifetime()
                .Build()
                .RunAsync();
        }

        private static void ConfigureHost(IConfigurationBuilder configurationBuilder, string[] args)
        {
            configurationBuilder.SetBasePath(Directory.GetCurrentDirectory());
            configurationBuilder.AddJsonFile("configuration/hostsettings.json", optional: true);
            configurationBuilder.AddCommandLine(args);
        }

        private static void ConfigureApp(
            HostBuilderContext hostContext, 
            IConfigurationBuilder configurationBuilder, 
            string[] args)
        {
            configurationBuilder.AddJsonFile("configuration/appsettings.json", optional: true);
            configurationBuilder.AddJsonFile(
                $"configuration/appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json",
                optional: true);
            configurationBuilder.AddCommandLine(args);
        }

        private static void ConfigureServices(HostBuilderContext hostContext, IServiceCollection services)
        {
            services.AddLogging();
            services.AddSingleton(typeof(IOutput), typeof(Output));
            services.AddSingleton(typeof(IApplicationService), typeof(ApplicationService));
            services.AddHostedService<HostedService>();
        }

        private static void ConfigureLogging(HostBuilderContext hostContext, ILoggingBuilder loggingBuilder)
        {
            loggingBuilder.AddConsole();
            loggingBuilder.AddDebug();
        }
    }
}