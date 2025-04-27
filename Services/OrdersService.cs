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
            if (order.Items.Count == 0) throw new MissingFieldException("empty items");

            if (string.IsNullOrEmpty(order.ClientUserId)) throw new MissingFieldException("missing client");

            await _ordersRepository.AddOrderAsync(order);
        }

        public async Task<Order> GetOrderAsync(string id)
        {
            return await _ordersRepository.GetOrderAsync(id);
        }
    }
}
