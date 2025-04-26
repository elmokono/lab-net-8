namespace MyAwsApp.Controllers
{
    public class ProductsController
    {
        public void MapUsersEndpoints(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/products/{id}", async (string id, IProductsService productsService) =>
            {
                var product = await productsService.GetProductAsync(id);

                if (product == null) return Results.NotFound();

                return Results.Created($"/api/products/{product.ProductId}", product);
            });
        }
    }
}

