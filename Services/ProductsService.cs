using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2;

namespace MyAwsApp.Services
{
    public interface IProductsService
    {
        Task AddProductAsync(Product product);
        Task<Product?> GetProductAsync(string id);
    }

    public class ProductsService : IProductsService
    {
        private readonly IProductsRepository _productsRepository;

        public ProductsService(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        public async Task AddProductAsync(Product product)
        {
            await _productsRepository.AddProductAsync(product);
        }

        public async Task<Product?> GetProductAsync(string id)
        {
            return await _productsRepository.GetProductAsync(id);
        }
    }
}
