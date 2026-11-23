using GeekShooping.MessageBus;

namespace GeekShooping.CartApi.RabbitMqSender
{
    public interface IRabbitMqMessageSender
    {
        void SendMessage(BaseMessage baseMessage, string queueName);
    }
}
