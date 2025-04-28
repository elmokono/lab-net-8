using Moq;
using MyAwsApp.Dtos;
using MyAwsApp.Repositories;
using MyAwsApp.Services;

namespace TestProject1
{
    public class UsersTests
    {
        private readonly Mock<IUsersRepository> _moqUsersRepository;
        private readonly IUsersService _usersService;

        public UsersTests()
        {
            _moqUsersRepository = new Mock<IUsersRepository>();
            _usersService = new UsersService(_moqUsersRepository.Object);
        }

        [Fact]
        public async Task GetUserId_MustExist()
        {
            //arrange
            var userId = "1";
            var expectedUser = new UserDto { Email = "contoso@hotmail.com", UserId = userId, Name = "Joe Contoso" };

            _moqUsersRepository
                .Setup(x => x.GetUserByIdAsync(userId))
                .ReturnsAsync(expectedUser);

            //act
            var result = await _usersService.GetUserByIdAsync(userId);


            //assert
            Assert.NotNull(result);
            Assert.Equal(expectedUser.Email, result.Email);
        }
    }
}