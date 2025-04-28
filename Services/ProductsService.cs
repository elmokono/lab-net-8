namespace MyAwsApp.Services
{
    public interface IProductsService
    {
        Task AddProductAsync(ProductDto product);
        Task<ProductDto?> GetProductAsync(string id);
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
    }
}
