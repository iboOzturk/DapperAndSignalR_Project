using DapperAndSignalR_Project.API.DTOs;

namespace DapperAndSignalR_Project.API.Repositories
{
    public interface IProductRepository
    {
        Task<List<ResultProductDto>> GetAllProductAsync();
        Task<ResultProductDto> GetProductAsync(int id);
        void CreateProductAsync(CreateProductDto createProductDto);
        void DeleteProductAsync(int id);
        void UpdateProductAsync(UpdateProductDto updateProductDto);
    }
}
