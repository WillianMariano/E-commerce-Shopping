using GeekShooping.CartApi.Repository;
using GeekShooping.OrderApi.Messages;
using GeekShooping.OrderApi.Model;
using GeekShooping.OrderApi.RabbitMqSender;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace GeekShooping.OrderApi.MessageConsumer
{
    public class RabbitMqCheckoutConsumer : BackgroundService
    {
        private readonly OrderRepository _repository;
        private IConnection _connection;
        private IModel _channel;
        private IRabbitMqMessageSender _rabbitMqMessageSender;
        public RabbitMqCheckoutConsumer(OrderRepository repository, IRabbitMqMessageSender rabbitMqMessageSender)
        {
            _repository = repository;
            _rabbitMqMessageSender = rabbitMqMessageSender;
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };
            _connection = factory.CreateConnection();

            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: "checkoutqueue", false, false, false, arguments: null);
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (chanel, evt) =>
            {
                var content = Encoding.UTF8.GetString(evt.Body.ToArray());
                CheckoutHeaderDto dto = JsonSerializer.Deserialize<CheckoutHeaderDto>(content);
                ProcessOrder(dto).GetAwaiter().GetResult();
                _channel.BasicAck(evt.DeliveryTag, false);
            };
            _channel.BasicConsume("checkoutqueue", false, consumer);
            return Task.CompletedTask;
        }

        private async Task ProcessOrder(CheckoutHeaderDto dto)
        {
            OrderHeader order = new()
            {
                UserId = dto.UserId,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                OrderDetails = new List<OrderDetail>(),
                CardNumber = dto.CardNumber,
                CouponCode = dto.CouponCode,
                Cvv = dto.Cvv,
                DiscountAmount = dto.DiscountTotal,
                Email = dto.Email,
                ExpiryMothYear = dto.ExpiryMothYear,
                OrderTime = DateTime.Now,
                PurchaseAmount = dto.PurchaseAmount,
                PaymentStatus = false,
                Phone = dto.Phone,
                DateTime = dto.DateTime,
            };

            foreach (var details in dto.CartDetails)
            {
                OrderDetail detail = new()
                {
                    ProductId = details.ProductId,
                    ProductName = details.Product.Name,
                    Price = details.Product.Price,
                    Count = details.Count
                };
                order.CartTotalItens += details.Count;
                order.OrderDetails.Add(detail);
            }

            await _repository.AddOrder(order);

            PaymentDto payment = new()
            {
                Name = order.FirstName + " " + order.LastName,
                CardNumber = order.CardNumber,
                Cvv = order.Cvv,
                ExpiryMonthYear = order.ExpiryMothYear,
                OrderId = order.Id,
                PurchaseAmout = order.PurchaseAmount,
                Email = order.Email,
            };
            try
            {
                _rabbitMqMessageSender.SendMessage(payment, "orderpaymentprocessqueue");
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
