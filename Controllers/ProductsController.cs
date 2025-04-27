using MyAwsApp.Models;
using MyAwsApp.Services;

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


            app.MapPost("/api/products", async (Product product, IProductsService productsService) =>
            {
                await productsService.AddProductAsync(product);

                return Results.Created($"/api/products/{product.ProductId}", product);

            });
        }
    }
}

