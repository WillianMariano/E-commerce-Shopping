using GeekShooping.MessageBus;

namespace GeekShooping.PaymentApi.Messages
{
    public class UpdatePaymentResultMessage:BaseMessage
    {
        public long OrderId { get; set; }
        public bool Status { get; set; } 
        public string Email { get; set; }
    }
}
