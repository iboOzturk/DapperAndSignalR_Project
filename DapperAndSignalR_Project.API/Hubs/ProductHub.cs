using DapperAndSignalR_Project.API.Repositories;
using Microsoft.AspNetCore.SignalR;

namespace DapperAndSignalR_Project.API.Hubs
{
    public class ProductHub:Hub
    {
        private readonly IProductRepository _repository;

        public ProductHub(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task GetProductList()
        {
            var values=await _repository.GetAllProductAsync();
            await Clients.All.SendAsync("ReceiveProductList",values);
        }
    }
}
