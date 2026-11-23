using GeekShooping.CartApi.Data.DTOs;
using GeekShooping.CartApi.Messages;
using GeekShooping.CartApi.RabbitMqSender;
using GeekShooping.CartApi.Repository;
using GeekSooping.CartApi.Data.DTOs;
using GeekSooping.CartApi.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeekShooping.CartApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CartController : ControllerBase
    {
        private ICartRepository _cartRepository;
        private ICouponRepository _couponRepository;
        private IRabbitMqMessageSender _rabbitMqMessageSender;
        public CartController(ICartRepository cartRepository, ICouponRepository couponRepository, IRabbitMqMessageSender rabbitMqMessageSender)
        {
            _cartRepository = cartRepository ?? throw new ArgumentException(nameof(cartRepository));
            _couponRepository = couponRepository ?? throw new ArgumentException(nameof(couponRepository));
            _rabbitMqMessageSender = rabbitMqMessageSender ?? throw new ArgumentException(nameof(rabbitMqMessageSender));
        }
        [HttpGet("find-cart/{id}")]
        public async Task<ActionResult<CartDto>> FindById(string id)
        {
            var cart = await _cartRepository.FindCartByUserid(id);
            if (cart == null) return NotFound();
            return Ok(cart);
        }
        [HttpPost("add-cart")]
        public async Task<ActionResult<CartDto>> AddCart(CartDto cartDto)
        {
            var cart = await _cartRepository.SaveOrUpdateCart(cartDto);
            if (cart == null) return NotFound();
            return Ok(cart);
        }
        [HttpPut("update-cart")]
        public async Task<ActionResult<CartDto>> UpdateCart(CartDto cartDto)
        {
            var cart = await _cartRepository.SaveOrUpdateCart(cartDto);
            if (cart == null) return NotFound();
            return Ok(cart);
        }
        [HttpDelete("remove-cart/{id}")]
        public async Task<ActionResult<CartDto>> RemoveCart(int id)
        {
            var status = await _cartRepository.RemoveFromCart(id);
            if (!status) return BadRequest();
            return Ok(status);
        }

        [HttpPost("apply-coupon")]
        public async Task<ActionResult<CartDto>> ApplyCoupon(CartDto cartDto)
        {
            var status = await _cartRepository.ApplyCoupon(cartDto.CartHeader.UserId, cartDto.CartHeader.CouponCode);
            if (!status) return NotFound();
            return Ok(status);
        }
        [HttpDelete("remove-coupon/{userId}")]
        public async Task<ActionResult<CartDto>> RemoveCoupon(string userId)
        {
            var status = await _cartRepository.RemoveCoupon(userId);
            if (!status) return NotFound();
            return Ok(status);
        }
        [HttpPost("checkout")]
        public async Task<ActionResult<CheckoutHeaderDto>> Checkout(CheckoutHeaderDto dto)
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            if (dto?.UserId == null) return BadRequest();
            var cart = await _cartRepository.FindCartByUserid(dto.UserId);
            if (cart == null) return NotFound();

            if (!string.IsNullOrEmpty(dto.CouponCode))
            {
                CouponDto coupon = await _couponRepository.GetCouponByCouponCode(dto.CouponCode, token);
                if (dto.DiscountTotal != coupon.DiscountAmount)
                {
                    return StatusCode(412);
                }
            }

            dto.CartDetails = cart.CartDetails;
            dto.DateTime = DateTime.Now;

            _rabbitMqMessageSender.SendMessage(dto, "checkoutqueue");

            await _cartRepository.ClearCart(dto.UserId);
            return Ok(dto);
        }
    }
}
