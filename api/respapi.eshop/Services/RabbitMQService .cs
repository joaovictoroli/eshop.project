using System;
using System.Text;
using System.Threading.Tasks;
using respapi.eshop.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace respapi.eshop.Services
{
    public class RabbitMQService : IMessageQueueService, IDisposable
    {
        private readonly IConnection _connection;
        private IModel _channel; 

        public RabbitMQService()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            _connection = factory.CreateConnection();
        }

        public async Task PublishMessage(string queueName, string message)
        {
            await Task.Yield();

            using (var channel = _connection.CreateModel())
            {
                channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body);
            }
        }

        public async Task<string> ConsumeMessage(string queueName)
        {
            await Task.Yield(); 

            using (var channel = _connection.CreateModel())
            {
                var data = channel.BasicGet(queueName, true);
                if (data != null)
                {
                    return Encoding.UTF8.GetString(data.Body.ToArray());
                }
                return null;
            }
        }

        public void Subscribe(string queueName, Action<string> onReceive)
        {
            if(_channel == null || _channel.IsClosed)
            {
                _channel = _connection.CreateModel();
            }

            _channel.QueueDeclare(queue: queueName,
                                durable: false,
                                exclusive: false,
                                autoDelete: false,
                                arguments: null);

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                onReceive(message);
            };

            _channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
        }

        public void Dispose()
        {
            _channel?.Close();
            _connection?.Close();
            _connection?.Dispose();
        }
    }
}
