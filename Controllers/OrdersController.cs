namespace MyAwsApp.Controllers
{
    public class OrdersController
    {
        public void MapUsersEndpoints(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/orders/{id}", async (string id, IOrdersService ordersService) =>
            {

                var order = await ordersService.GetOrderAsync(id);

                if (order == null) return Results.NotFound();

                return Results.Ok(order);

            });

            app.MapPost("/api/orders", async (OrderDto order, IOrdersService ordersService) =>
            {

                await ordersService.AddOrderAsync(order);

                return Results.Created($"/api/orders/{order.OrderId}", order);

            });
        }
    }
}
