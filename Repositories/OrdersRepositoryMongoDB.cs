using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace MyAwsApp.Repositories
{
    public interface IOrdersRepository
    {
        Task AddOrderAsync(OrderDto order);
        Task<OrderDto> GetOrderAsync(string id);
    }

    public class OrdersRepositoryMongoDB : IOrdersRepository
    {
        private readonly IMongoCollection<OrderDto> _ordersCollection;
        private readonly IMongoDatabase _ordersDatabase;

        public OrdersRepositoryMongoDB(IOptions<MongoDBSettings> settings, IMongoClient mongoClient)
        {
            _ordersDatabase = mongoClient.GetDatabase(settings.Value.DatabaseName);
            _ordersCollection = _ordersDatabase.GetCollection<OrderDto>(settings.Value.OrdersCollection);
        }

        public async Task AddOrderAsync(OrderDto order)
        {
            await _ordersCollection.InsertOneAsync(order);
        }

        public async Task<OrderDto> GetOrderAsync(string id)
        {
            //var orders = _ordersCollection.Find(f => f.OrderId != "").ToList();

            return await _ordersCollection.Find(f => f.OrderId == id).FirstOrDefaultAsync();
        }
    }
}
