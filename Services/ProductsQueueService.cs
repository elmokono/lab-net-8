using Amazon.SQS;
using Amazon.SQS.Model;
using System.Text.Json;

namespace MyAwsApp.Services
{
    public interface IProductsQueueService
    {
        Task CreateProductAsync(ProductDto product);
    }

    public class ProductsQueueService : IProductsQueueService
    {
        private readonly IAmazonSQS _sqs;
        private readonly string _queueUrl;

        public ProductsQueueService(IAmazonSQS sqs, IConfiguration config)
        {
            _sqs = sqs;
            _queueUrl = config["SQSSettings:ProductsCreationQueue"];
        }

        public async Task CreateProductAsync(ProductDto product)
        {
            var body = JsonSerializer.Serialize(product);
            var message = new SendMessageRequest
            {
                QueueUrl = _queueUrl,
                MessageBody = body,
            };

            await _sqs.SendMessageAsync(message);
        }
    }
}
