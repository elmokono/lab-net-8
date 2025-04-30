namespace MyAwsApp.Services
{
    public interface IProductsService
    {
        Task AddProductAsync(ProductDto product);
        Task<ProductDto?> GetProductAsync(string id);
        Task<List<ProductDto>> GetProductsAsync(string pattern);
    }

    public class ProductsService : IProductsService
    {
        private readonly IProductsRepository _productsRepository;

        public ProductsService(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        public async Task AddProductAsync(ProductDto product)
        {
            await _productsRepository.AddProductAsync(product);
        }

        public async Task<ProductDto?> GetProductAsync(string id)
        {
            return await _productsRepository.GetProductAsync(id);
        }

        public async Task<List<ProductDto>> GetProductsAsync(string pattern)
        {
            return await _productsRepository.GetProductsAsync(pattern);
        }
    }
}
