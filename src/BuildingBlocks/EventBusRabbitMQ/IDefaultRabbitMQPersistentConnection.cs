using RabbitMQ.Client;

namespace EventBusRabbitMQ
{
    public interface IDefaultRabbitMQPersistentConnection
    {
        bool IsConnected { get; }

        IModel CreateModel();
        void Dispose();
        bool TryConnect();
    }
}