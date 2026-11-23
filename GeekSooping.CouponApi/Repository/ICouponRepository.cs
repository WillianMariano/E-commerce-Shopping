using GeekSooping.CouponApi.Data.DTOs;

namespace GeekSooping.CouponApi.Repository
{
    public interface ICouponRepository
    {
        Task<CouponDto> GetCouponByCouponCode(string couponCode);
    }
}
