using GeekShooping.OrderApi.Model;

namespace GeekShooping.CartApi.Repository
{
    public interface IOrderRepository
    { 
        Task<bool> AddOrder(OrderHeader header);
        Task UpdateOrderPaymentStatus(long orderHeaderId, bool paid); 
    }
}
