using CryptoAddressGeneratorBTC.Core.Events;
using CryptoAddressGeneratorBTC.Core.Pipelines;
using CryptoAddressGeneratorBTC.Infrastructure.Events;
using CryptoAddressGeneratorBTC.Infrastructure.Metrics;
using CryptoAddressGeneratorBTC.Infrastructure.Persistence;
using CryptoAddressGeneratorBTC.Infrastructure.Validation;
using Microsoft.Extensions.DependencyInjection;

namespace CryptoAddressGeneratorBTC.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDomainInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton(typeof(IJsonRepository<>), typeof(JsonRepository<>));
            services.AddSingleton<IRequestValidator<object>, DefaultRequestValidator<object>>();
            services.AddSingleton<IMetricsPublisher, ConsoleMetricsPublisher>();
            services.AddSingleton<IDomainEventBus, InMemoryDomainEventBus>();
            services.AddSingleton(typeof(IPipelineBehavior<,>), typeof(LoggingPipelineBehavior<,>));
            return services;
        }
    }
}
