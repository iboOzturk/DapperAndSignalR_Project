﻿using DapperAndSignalR_Project.API.DTOs;
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
        [HttpPost]
        public IActionResult CreateProduct(CreateProductDto createProductDto)
        {
            _repository.CreateProductAsync(createProductDto);
            return Ok("Başarılı bir şekilde oluşturuldu");
        }
        [HttpDelete]
        public IActionResult DeleteProduct(int id)
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