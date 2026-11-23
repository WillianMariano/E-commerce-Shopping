using GeekSooping.CartApi.Data.DTOs;
using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;

namespace GeekSooping.CartApi.Repository
{
    public class CouponRepository : ICouponRepository
    {
        private readonly HttpClient _client;

        public CouponRepository(HttpClient client)
        {
            _client = client;
        }

        public async Task<CouponDto> GetCouponByCouponCode(string couponCode, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.GetAsync("api/v1/coupon/" + couponCode);
            var content = await response.Content.ReadAsStringAsync();
            if (response.StatusCode != HttpStatusCode.OK)
            {
                return new CouponDto();
            }
            return JsonSerializer.Deserialize<CouponDto>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}
