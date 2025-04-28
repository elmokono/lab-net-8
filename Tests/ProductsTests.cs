using Moq;
using MyAwsApp.Dtos;
using MyAwsApp.Repositories;
using MyAwsApp.Services;

namespace TestProject1
{
    public class ProductsTests
    {
        private readonly Mock<IProductsRepository> _moqProductsRepository;
        private readonly IProductsService _productsService;

        public ProductsTests()
        {
            _moqProductsRepository = new Mock<IProductsRepository>();
            _productsService = new ProductsService(_moqProductsRepository.Object);
        }

        [Fact]
        public async Task GetProductId_MustExist()
        {
            //arrange
            var productId = "0001";
            var expectedProduct = new ProductDto {
                Description = "test1",
                Name = "t1",
                Price = 100,
                ProductId = productId,
                Stock = 99
            };

            _moqProductsRepository
                .Setup(x => x.GetProductAsync(productId))
                .ReturnsAsync(expectedProduct);

            //act
            var result = await _productsService.GetProductAsync(productId);


            //assert
            Assert.NotNull(result);
            Assert.Equal(expectedProduct.Name, result.Name);
        }

    }
}
