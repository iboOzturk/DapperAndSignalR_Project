using Dapper;
using DapperAndSignalR_Project.API.DTOs;
using DapperAndSignalR_Project.API.Hubs;
using DapperAndSignalR_Project.API.Models;
using Microsoft.AspNetCore.SignalR;

namespace DapperAndSignalR_Project.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DapperContext _context;
        private readonly IHubContext<ProductHub> _hubContext;

        public ProductRepository(DapperContext context, IHubContext<ProductHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
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
                var values=await GetAllProductAsync();
                await _hubContext.Clients.All.SendAsync("ReceiveProductList", values);
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
                var values = await GetAllProductAsync();
                await _hubContext.Clients.All.SendAsync("ReceiveProductList", values);
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

        public async Task<ResultProductDto> GetProductAsync(int id)
        {
            string query = "Select * from Products where ProductID=@ProductID";
            var parameter = new DynamicParameters();
            parameter.Add("@ProductID", id);
            using (var connection = _context.CreateConnection())
            {
                var value = await connection.QueryFirstOrDefaultAsync<ResultProductDto>(query, parameter);

                return value;
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
