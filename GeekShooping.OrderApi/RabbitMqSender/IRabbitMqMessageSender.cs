using GeekShooping.MessageBus;

namespace GeekShooping.OrderApi.RabbitMqSender
{
    public interface IRabbitMqMessageSender
    {
        void SendMessage(BaseMessage baseMessage, string queueName);
    }
}
