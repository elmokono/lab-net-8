using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;

namespace MyAwsApp.Repositories
{
    public interface IProductsRepository
    {
        Task AddProductAsync(Product product);
        Task<Product?> GetProductAsync(string id);
    }

    public class ProductsRepositoryDynDB : IProductsRepository
    {
        private readonly AmazonDynamoDBClient _amazonDynamoDBClient;
        private readonly Table _tableProducts;

        public ProductsRepositoryDynDB()
        {
            _amazonDynamoDBClient = new AmazonDynamoDBClient();
            _tableProducts = Table.LoadTable(_amazonDynamoDBClient, "Products");
        }

        public async Task AddProductAsync(Product product)
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

        public async Task<Product?> GetProductAsync(string id)
        {
            var product = await _tableProducts.GetItemAsync(id);

            if (product == null) return null;

            return new Product
            {
                Description = product["Description"],
                Name = product["Name"],
                Price = (decimal)product["Price"],
                ProductId = id,
                Stock = (int)product["Stock"]
            };
        }
    }
}
