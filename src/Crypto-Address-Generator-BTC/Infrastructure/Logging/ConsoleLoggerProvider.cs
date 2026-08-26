using Microsoft.Extensions.Logging;

namespace CryptoAddressGeneratorBTC.Infrastructure.Logging
{
    public class ConsoleLoggerProvider : ILoggerProvider
    {
        public ILogger CreateLogger(string categoryName) => new ConsoleLogger(categoryName);
        public void Dispose() { }
    }
}
