namespace DocSync.API.Models
{
    public class MessageQueueSettings
    {
        public string Host { get; set; } = string.Empty;
        public string QueueName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty ;
        public string Password { get; set; } = string.Empty;
        public string VirtualHost { get; set; } = string.Empty;
    }
}
