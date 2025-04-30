using FluentValidation;

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


            app.MapPost("/api/products", async (ProductDto product, IValidator<ProductDto> validator, IProductsService productsService) =>
            {
                var validation = await validator.ValidateAsync(product);

                if (!validation.IsValid) return Results.ValidationProblem(validation.ToDictionary());

                await productsService.AddProductAsync(product);

                return Results.Created($"/api/products/{product.ProductId}", product);

            });

            app.MapGet("/api/products/search/{pattern}", async (string pattern, IProductsService productsService) =>
            {
                var products = await productsService.GetProductsAsync(pattern);

                return Results.Ok(products);
            });
        }
    }
}

