using System.Threading.Tasks;

namespace respapi.eshop.Interfaces
{
    public interface IMessageQueueService
    {
        Task PublishMessage(string queueName, string message);
        Task<string> ConsumeMessage(string queueName);
        void Subscribe(string queueName, Action<string> onReceive);
    }
}
