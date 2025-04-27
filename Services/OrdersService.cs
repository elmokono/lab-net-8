namespace MyAwsApp.Services
{
    public interface IOrdersService
    {
        Task AddOrderAsync(Order order);
        Task<Order> GetOrderAsync(string id);
    }

    public class OrdersService : IOrdersService
    {
        private readonly IOrdersRepository _ordersRepository;

        public OrdersService(IOrdersRepository ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }

        public async Task AddOrderAsync(Order order)
        {
            await _ordersRepository.AddOrderAsync(order);
        }

        public async Task<Order> GetOrderAsync(string id)
        {
            return await _ordersRepository.GetOrderAsync(id);
        }
    }
}
