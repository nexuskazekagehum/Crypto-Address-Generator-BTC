using CryptoAddressGeneratorBTC.Core.Configuration;
using CryptoAddressGeneratorBTC.Core.Services;
using CryptoAddressGeneratorBTC.Core.Utils;
using CryptoAddressGeneratorBTC.Infrastructure.Configuration;
using CryptoAddressGeneratorBTC.Infrastructure.ConsoleUi;
using CryptoAddressGeneratorBTC.Infrastructure.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CryptoAddressGeneratorBTC
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.Title = "CryptoAddressGeneratorBTC";
            var arguments = ArgumentParser.Parse(args);
            var services = ConfigureServices();
            var serviceProvider = services.BuildServiceProvider();
            var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
            var cryptoModule = serviceProvider.GetRequiredService<ICryptoModule>();
            var healthChecker = serviceProvider.GetRequiredService<IHealthChecker>();
            var menuRenderer = serviceProvider.GetRequiredService<MenuRenderer>();

            logger.LogInformation("Console module started");
            await healthChecker.CheckAsync(CancellationToken.None);
            PrintBanner();
            await RunInteractiveLoop(cryptoModule, menuRenderer, logger, CancellationToken.None);
        }

        static IServiceCollection ConfigureServices()
        {
            var services = new ServiceCollection();
            var configuration = ConfigurationLoader.Build(Array.Empty<string>());
            services.AddSingleton(configuration);
            services.AddSingleton(configuration.BindOptions());
            services.AddLogging(builder => builder.AddProvider(new ConsoleLoggerProvider()));
            services.AddSingleton<IDataProvider, SimulatedDataProvider>();
            services.AddSingleton<IRepository, InMemoryRepository>();
            services.AddSingleton<IHealthChecker, EndpointHealthChecker>();
            services.AddSingleton<MenuRenderer>();
            services.AddSingleton<ICryptoModule, CryptoModule>();
            return services;
        }

        static void PrintBanner()
        {
            System.Console.WriteLine("Module initialized.");
        }

        static async Task RunInteractiveLoop(ICryptoModule cryptoModule, MenuRenderer menuRenderer, ILogger logger, CancellationToken cancellationToken)
        {
            var menuOptions = new[]
            {
                "Run simulation",
                "Show last snapshot",
                "Add input parameter",
                "Export results",
                "Exit"
            };
            while (true)
            {
                menuRenderer.RenderHeader("CryptoAddressGeneratorBTC - Console Module");
                menuRenderer.RenderMenu(menuOptions);
                var choice = System.Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        System.Console.Write("Symbol: ");
                        var symbol = System.Console.ReadLine() ?? "BTC";
                        await cryptoModule.SimulateAsync(symbol, cancellationToken);
                        break;
                    case "2":
                        var snapshot = await cryptoModule.GetLatestSnapshotAsync(cancellationToken);
                        System.Console.WriteLine($"Snapshot contains {snapshot.Results.Count} results");
                        break;
                    case "3":
                        logger.LogWarning("Parameter input is not implemented in this demo");
                        break;
                    case "4":
                        logger.LogWarning("Export is not implemented in this demo");
                        break;
                    case "5":
                        return;
                    default:
                        logger.LogWarning("Invalid choice");
                        break;
                }
            }
        }
    }
}
