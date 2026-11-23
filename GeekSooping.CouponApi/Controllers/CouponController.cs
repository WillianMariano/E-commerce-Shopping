using GeekSooping.CouponApi.Data.DTOs;
using GeekSooping.CouponApi.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeekSooping.CouponApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CouponController : ControllerBase
    {
        private ICouponRepository _repository;
        public CouponController(ICouponRepository repository)
        {
            _repository = repository ?? throw new ArgumentException(nameof(repository));
        } 
        [HttpGet("{couponCode}")]
        [Authorize]
        public async Task<ActionResult<CouponDto>> GetCouponByCouponCode(string couponCode)
        {
            var coupon = await _repository.GetCouponByCouponCode(couponCode);
            if (coupon?.Id <= 0) return NotFound();
            return Ok(coupon);
        }
    }
}
