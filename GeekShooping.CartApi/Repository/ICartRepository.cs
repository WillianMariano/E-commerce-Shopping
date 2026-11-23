using GeekShooping.CartApi.Data.DTOs;

namespace GeekShooping.CartApi.Repository
{
    public interface ICartRepository
    {
        Task<CartDto> FindCartByUserid(string userid);
        Task<CartDto> SaveOrUpdateCart(CartDto cartDto);
        Task<bool> RemoveFromCart(long cartDetailsId);
        Task<bool> ApplyCoupon(string userId, string couponCode);
        Task<bool> RemoveCoupon(string userId);
        Task<bool> ClearCart(string userId);
    }
}
