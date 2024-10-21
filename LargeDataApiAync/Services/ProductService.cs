using LargeDataApiAync.Models;

namespace LargeDataApiAync.Services
{
    public class ProductService
    {
        private readonly ProductRepository _repository;

        public ProductService(ProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Product>> GetProductsAsync(int pageNumber, int pageSize)
        {
            return await _repository.GetProductsAsync(pageNumber, pageSize);
        }
    }
}
