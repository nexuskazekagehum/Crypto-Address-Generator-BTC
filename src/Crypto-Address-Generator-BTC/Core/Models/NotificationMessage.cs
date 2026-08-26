namespace CryptoAddressGeneratorBTC.Core.Models
{
    public class NotificationMessage
    {
        public string MessageId { get; set; } = Guid.NewGuid().ToString();
        public string Channel { get; set; } = "console";
        public string Recipient { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public int Priority { get; set; } = 1;
        public bool Delivered { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
