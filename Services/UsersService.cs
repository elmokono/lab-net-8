namespace MyAwsApp.Services
{
    public interface IUsersService
    {
        Task AddUserAsync(UserDto user);
        Task<UserDto?> GetUserByIdAsync(string id);
        Task HydrateDB(int count);
    }

    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;

        public UsersService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task AddUserAsync(UserDto user)
        {
            await _usersRepository.AddUserAsync(user);
        }

        public async Task<UserDto?> GetUserByIdAsync(string id)
        {
            return await _usersRepository.GetUserByIdAsync(id);
        }

        public async Task HydrateDB(int count)
        {
            await _usersRepository.HydrateDB(count);
        }
    }
}
