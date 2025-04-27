using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace MyAwsApp.Repositories
{
    public interface IOrdersRepository
    {
        Task AddOrderAsync(Order order);
        Task<Order> GetOrderAsync(string id);
    }

    public class OrdersRepositoryMongoDB : IOrdersRepository
    {
        private readonly IMongoCollection<Order> _ordersCollection;
        private readonly IMongoDatabase _ordersDatabase;

        public OrdersRepositoryMongoDB(IOptions<MongoDBSettings> settings, IMongoClient mongoClient)
        {
            _ordersDatabase = mongoClient.GetDatabase(settings.Value.DatabaseName);
            _ordersCollection = _ordersDatabase.GetCollection<Order>(settings.Value.OrdersCollection);
        }

        public async Task AddOrderAsync(Order order)
        {
            await _ordersCollection.InsertOneAsync(order);
        }

        public async Task<Order> GetOrderAsync(string id)
        {
            //var orders = _ordersCollection.Find(f => f.OrderId != "").ToList();

            return await _ordersCollection.Find(f => f.OrderId == id).FirstOrDefaultAsync();
        }
    }
}
