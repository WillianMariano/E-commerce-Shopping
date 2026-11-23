using GeekShooping.MessageBus;

namespace GeekShooping.PaymentApi.RabbitMqSender
{
    public interface IRabbitMqMessageSender
    {
        void SendMessage(BaseMessage baseMessage);
    }
}
