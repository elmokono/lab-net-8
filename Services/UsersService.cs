using MyAwsApp.Models;
using MyAwsApp.Repositories;

namespace MyAwsApp.Services
{
    public interface IUsersService
    {
        Task AddUserAsync(User user);
        Task<User?> GetUserByIdAsync(string id);
        Task HydrateDB(int count);
    }

    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;

        public UsersService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task AddUserAsync(User user)
        {
            await _usersRepository.AddUserAsync(user);
        }

        public async Task<User?> GetUserByIdAsync(string id)
        {
            return await _usersRepository.GetUserByIdAsync(id);
        }

        public async Task HydrateDB(int count)
        {
            await _usersRepository.HydrateDB(count);
        }
    }
}
