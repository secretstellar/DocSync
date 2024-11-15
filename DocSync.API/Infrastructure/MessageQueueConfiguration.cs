using System.Data.SqlClient;
using System.Data;
using RabbitMQ.Client;
using DocSync.API.Models;
using Microsoft.Extensions.Options;
using System.Threading.Channels;

namespace DocSync.API.Infrastructure
{
    public class MessageQueueConfiguration
    {
        private readonly MessageQueueSettings _msgQueueSettings;
        private readonly IConfiguration _configuration;

        public MessageQueueConfiguration(IConfiguration configuration, IOptions<MessageQueueSettings> msgQueueSettings)
        {
            _configuration = configuration;
            _msgQueueSettings = msgQueueSettings.Value;
        }

        public IConnection CreateConnection()
        {
            var factory = new ConnectionFactory()
            {
                HostName = _msgQueueSettings.Host,
                UserName = _msgQueueSettings.UserName,
                Password = _msgQueueSettings.Password,
                VirtualHost = _msgQueueSettings.VirtualHost
            };
            return factory.CreateConnection();
        }

        public IModel CreateChannel(IConnection connection)
        {
            var channel = connection.CreateModel();
            channel.QueueDeclare(queue: _msgQueueSettings.QueueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
            return channel; ;

        }
    }
}
