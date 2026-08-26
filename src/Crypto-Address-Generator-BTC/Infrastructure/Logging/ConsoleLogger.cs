using Microsoft.Extensions.Logging;

namespace CryptoAddressGeneratorBTC.Infrastructure.Logging
{
    public class ConsoleLogger : ILogger
    {
        private readonly string _category;

        public ConsoleLogger(string category)
        {
            _category = category;
        }

        public IDisposable? BeginScope<TState>(TState state) where TState : notnull => null;
        public bool IsEnabled(LogLevel logLevel) => logLevel != LogLevel.None;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            if (!IsEnabled(logLevel)) return;
            var color = logLevel switch
            {
                LogLevel.Error => ConsoleColor.Red,
                LogLevel.Warning => ConsoleColor.Yellow,
                LogLevel.Information => ConsoleColor.Green,
                _ => ConsoleColor.Gray
            };
            System.Console.ForegroundColor = color;
            System.Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{logLevel}] [{_category}] {formatter(state, exception)}");
            System.Console.ResetColor();
        }
    }
}
