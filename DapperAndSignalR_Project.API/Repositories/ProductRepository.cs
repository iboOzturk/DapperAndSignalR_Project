﻿using Dapper;
using DapperAndSignalR_Project.API.DTOs;
using DapperAndSignalR_Project.API.Models;

namespace DapperAndSignalR_Project.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DapperContext _context;

        public ProductRepository(DapperContext context)
        {
            _context = context;
        }

        public async void CreateProductAsync(CreateProductDto createProductDto)
        {
            string query = "insert into Products (Name,Description,CreateDate) values (@Name,@Description,@CreateDate)";
            var parametres = new DynamicParameters();
            parametres.Add("@Name", createProductDto.Name);
            parametres.Add("@Description", createProductDto.Description);
            parametres.Add("@CreateDate", DateTime.Now);
            using (var connection=_context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parametres);
            }
        }

        public async void DeleteProductAsync(int id)
        {
            string query = "Delete from Products where ProductID=@ProductID";
            var parameters = new DynamicParameters();
            parameters.Add("@ProductID", id);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<List<ResultProductDto>> GetAllProductAsync()
        {
            string query = "Select * from Products";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultProductDto>(query);
                return values.ToList();
            }
        }

        public async void UpdateProductAsync(UpdateProductDto updateProductDto)
        {
            string query = "Update Products " +
                "set Name=@Name,Description=@Description " +
                "where ProductId=@ProductId ";
            var parameters = new DynamicParameters();
            parameters.Add("@Name", updateProductDto.Name);
            parameters.Add("@Description", updateProductDto.Description);                  
            parameters.Add("@ProductId", updateProductDto.ProductId);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}