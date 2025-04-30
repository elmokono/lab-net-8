using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;

namespace MyAwsApp.Repositories
{
    public interface IProductsRepository
    {
        Task AddProductAsync(ProductDto product);
        Task<ProductDto?> GetProductAsync(string id);
        Task<List<ProductDto>> GetProductsAsync(string pattern);
    }

    public class ProductsRepositoryDynDB : IProductsRepository
    {
        private readonly DynamoDBContext _dynamoDBContext;
        private readonly AmazonDynamoDBClient _amazonDynamoDBClient;
        private readonly Table _tableProducts;

        public ProductsRepositoryDynDB(AmazonDynamoDBClient amazonDynamoDBClient)
        {
            _amazonDynamoDBClient = amazonDynamoDBClient;
            _dynamoDBContext = new DynamoDBContext(_amazonDynamoDBClient);
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

        public async Task<List<ProductDto>> GetProductsAsync(string pattern)
        {
            var scanRequest = new ScanRequest { 
                TableName = "Products"
            };

            var products = await _amazonDynamoDBClient.ScanAsync(scanRequest);

            var conditions = new List<ScanCondition>() {
                new ScanCondition("Name", ScanOperator.Contains, pattern)
            };
            var allProducts = await _dynamoDBContext.ScanAsync<ProductDto>(conditions).GetRemainingAsync();

            return allProducts;
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
