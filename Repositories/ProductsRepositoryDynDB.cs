using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;

namespace MyAwsApp.Repositories
{
    public interface IProductsRepository
    {
        Task AddProductAsync(ProductDto product);
        Task<ProductDto?> GetProductAsync(string id);
    }

    public class ProductsRepositoryDynDB : IProductsRepository
    {
        private readonly AmazonDynamoDBClient _amazonDynamoDBClient;
        private readonly Table _tableProducts;

        public ProductsRepositoryDynDB(AmazonDynamoDBClient amazonDynamoDBClient)
        {
            _amazonDynamoDBClient = amazonDynamoDBClient;
            _tableProducts = Table.LoadTable(_amazonDynamoDBClient, "Products");
        }

        public async Task AddProductAsync(ProductDto product)
        {
            var newProduct = new Document
            {
                ["ProductId"] = product.ProductId,
                ["Name"] = product.Name,
                ["Description"] = product.Description,
                ["Price"] = product.Price,
                ["Stock"] = product.Stock
            };
            await _tableProducts.PutItemAsync(newProduct);
        }

        public async Task<ProductDto?> GetProductAsync(string id)
        {
            var product = await _tableProducts.GetItemAsync(id);

            if (product == null) return null;

            return new ProductDto
            {
                Description = product["Description"],
                Name = product["Name"],
                Price = (decimal)product["Price"],
                ProductId = id,
                Stock = (int)product["Stock"]
            };
        }

        public async Task Hydrate()
        {
            await AddProductAsync(new ProductDto { 
                Description = "My Demo Product",
                Name = "Product001",
                Price = (decimal)10.95,
                ProductId = "001",
                Stock = 90
            });
        }
    }
}
