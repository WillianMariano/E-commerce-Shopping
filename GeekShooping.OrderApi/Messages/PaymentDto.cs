using GeekShooping.MessageBus;

namespace GeekShooping.OrderApi.Messages
{
    public class PaymentDto : BaseMessage
    {
        public long OrderId { get; set; }
        public string Name { get; set; }
        public string CardNumber { get; set; }
        public string Cvv { get; set; }
        public string ExpiryMonthYear { get; set; }
        public decimal PurchaseAmout {  get; set; }
        public string Email { get; set; }
    }
}
