using DapperAndSignalR_Project.API.DTOs;
using DapperAndSignalR_Project.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DapperAndSignalR_Project.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repository;

        public ProductsController(IProductRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> ProductList()
        {
            var values= await _repository.GetAllProductAsync();
            return Ok(values);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var values= await _repository.GetProductAsync(id);
            return Ok(values);
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDto createProductDto)
        {
            _repository.CreateProductAsync(createProductDto);
            var values=await _repository.GetAllProductAsync();
            return Ok(values);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
             _repository.DeleteProductAsync(id);           
            return Ok("Silindi");
        }
        [HttpPut]
        public IActionResult UpdateProduct(UpdateProductDto updateProductDto)
        {
            _repository.UpdateProductAsync(updateProductDto);
            return Ok($"{updateProductDto.ProductId} numaralı ürün güncellendi");
        }
    }
}
