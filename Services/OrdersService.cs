namespace MyAwsApp.Services
{
    public interface IOrdersService
    {
        Task AddOrderAsync(OrderDto order);
        Task<OrderDto> GetOrderAsync(string id);
    }

    public class OrdersService : IOrdersService
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly IProductsService _productsService;

        public OrdersService(IOrdersRepository ordersRepository, IProductsService productsService)
        {
            _ordersRepository = ordersRepository;
            _productsService = productsService;
        }

        public async Task AddOrderAsync(OrderDto order)
        {
            if (order.Items.Count == 0) throw new MissingFieldException("empty items");

            if (string.IsNullOrEmpty(order.ClientUserId)) throw new MissingFieldException("missing client");

            //lookup product with latest info
            foreach (var item in order.Items)
            {
                var latestProduct = await _productsService.GetProductAsync(item.Product.ProductId) ?? throw new MissingMemberException($"Product {item.Product.ProductId} not found");
                if (latestProduct.Stock <= 0) throw new InvalidDataException($"Zero stock for {item.Product.ProductId}");

                item.Product.Name = latestProduct.Name;
                item.Product.Description = latestProduct.Description;
                item.Product.Price = latestProduct.Price;
                latestProduct.Stock -= 1;
                item.Product.Stock = latestProduct.Stock;

                //update stock
                await _productsService.AddProductAsync(latestProduct);
            }

            await _ordersRepository.AddOrderAsync(order);
        }

        public async Task<OrderDto> GetOrderAsync(string id)
        {
            return await _ordersRepository.GetOrderAsync(id);
        }
    }
}
