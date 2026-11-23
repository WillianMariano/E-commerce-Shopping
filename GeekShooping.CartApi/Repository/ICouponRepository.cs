using GeekSooping.CartApi.Data.DTOs;

namespace GeekSooping.CartApi.Repository
{
    public interface ICouponRepository
    {
        Task<CouponDto> GetCouponByCouponCode(string couponCode, string token);
    }
}
