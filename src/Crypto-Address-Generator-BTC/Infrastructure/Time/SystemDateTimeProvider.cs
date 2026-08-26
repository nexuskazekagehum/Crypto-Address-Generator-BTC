namespace CryptoAddressGeneratorBTC.Infrastructure.Time
{
    public interface IDateTimeProvider
    {
        DateTime Now { get; }
        DateTime UtcNow { get; }
        long UtcTicks { get; }
    }

    public class SystemDateTimeProvider : IDateTimeProvider
    {
        public DateTime Now => DateTime.Now;
        public DateTime UtcNow => DateTime.UtcNow;
        public long UtcTicks => DateTime.UtcNow.Ticks;
    }

    public class FixedDateTimeProvider : IDateTimeProvider
    {
        private readonly DateTime _fixed;

        public FixedDateTimeProvider(DateTime fixedTime)
        {
            _fixed = fixedTime.ToUniversalTime();
        }

        public DateTime Now => _fixed.ToLocalTime();
        public DateTime UtcNow => _fixed;
        public long UtcTicks => _fixed.Ticks;
    }
}
