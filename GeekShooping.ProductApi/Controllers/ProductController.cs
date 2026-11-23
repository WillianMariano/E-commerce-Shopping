using GeekShooping.ProductApi.Data.DTOs;
using GeekShooping.ProductApi.Repository;
using GeekShooping.ProductApi.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GeekShooping.ProductApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductRepository _repository;
        public ProductController(IProductRepository repository)
        {
            _repository = repository?? throw new ArgumentException(nameof(repository));
        }
        [HttpGet] 
        public async Task<ActionResult<IEnumerable<ProductDto>>> FindAll()
        {
            var products = await _repository.FindAll();
            return Ok(products);
        }
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<ProductDto>> FindById(long id)
        {
            var product = await _repository.FindById(id);
            if (product.Id <= 0) return NotFound();
            return Ok(product);
        }
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ProductDto>> Create([FromBody] ProductDto productDto)
        {
            if(productDto == null) return BadRequest();
            var productDtoCreated = await _repository.Create(productDto); 
            return Ok(productDtoCreated);
        }
        [HttpPut]
        [Authorize]
        public async Task<ActionResult<ProductDto>> Update([FromBody] ProductDto productDto)
        {
            if (productDto == null) return BadRequest();
            var productDtoUpdated = await _repository.Update(productDto);
            return Ok(productDtoUpdated);
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = Role.Admin)]

        public async Task<ActionResult> Delete(long id)
        {
            var status = await _repository.Delete(id);
            if (!status) return BadRequest();
            return Ok(status);
        }
    }
}
