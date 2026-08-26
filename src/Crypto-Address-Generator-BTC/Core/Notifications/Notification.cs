namespace CryptoAddressGeneratorBTC.Core.Notifications
{
    public interface INotification
    {
        Guid NotificationId { get; }
        DateTime OccurredAt { get; }
    }

    public interface INotificationHandler<TNotification> where TNotification : INotification
    {
        Task HandleAsync(TNotification notification, CancellationToken cancellationToken = default);
    }

    public abstract class DomainNotification : INotification
    {
        public Guid NotificationId { get; } = Guid.NewGuid();
        public DateTime OccurredAt { get; } = DateTime.UtcNow;
    }
}
