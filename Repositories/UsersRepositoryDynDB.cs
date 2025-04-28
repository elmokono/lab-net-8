using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;

namespace MyAwsApp.Repositories
{
    public interface IUsersRepository
    {
        Task AddUserAsync(UserDto user);
        Task<UserDto?> GetUserByIdAsync(string id);
        Task HydrateDB(int count);
    }

    public class UsersRepositoryDynDB : IUsersRepository
    {
        private readonly AmazonDynamoDBClient _amazonDynamoDBClient;
        private readonly Table _usersTable;

        public UsersRepositoryDynDB(AmazonDynamoDBClient amazonDynamoDBClient)
        {
            _amazonDynamoDBClient = amazonDynamoDBClient;
            _usersTable = Table.LoadTable(_amazonDynamoDBClient, "Users");
        }

        public async Task HydrateDB(int count)
        {
            var tables = await _amazonDynamoDBClient.ListTablesAsync();
            if (!tables.TableNames.Contains("Users"))
            {
                throw new KeyNotFoundException("Users table not found on dynamodb");
            }

            for (int i = 0; i < count; i++)
            {
                await AddUserAsync(
                    new UserDto
                    {
                        UserId = Guid.NewGuid().ToString(),
                        Name = $"{DateTime.Now.ToLongTimeString()}",
                        Email = $"{DateTime.Now.ToLongTimeString()}@hotmail.com"
                    });
            }
        }

        public async Task<UserDto?> GetUserByIdAsync(string id)
        {

            var document = await _usersTable.GetItemAsync(id);
            if (document == null) return null;

            return new UserDto
            {
                Email = document["Email"],
                UserId = id,
                Name = document["Name"]
            };
        }

        public async Task AddUserAsync(UserDto user)
        {

            var document = new Document
            {
                ["Email"] = user.Email,
                ["UserId"] = user.UserId,
                ["Name"] = user.Name,
            };

            await _usersTable.PutItemAsync(document);
        }
    }
}
