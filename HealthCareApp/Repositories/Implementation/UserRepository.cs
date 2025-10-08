using HealthCareApp.Models;
using HealthCareApp.Repositories.Interfaces;
using MongoDB.Driver;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace HealthCareApp.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _users;

        public UserRepository(IMongoClient client, IConfiguration config)
        {
            var database = client.GetDatabase(config.GetValue<string>("MongoDb:DatabaseName"));
            _users = database.GetCollection<User>("Users");
        }

        public async Task<User> GetUserByEmailAndRole(string email, string role)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Email, email) & Builders<User>.Filter.Eq(u => u.Role, role);
            return await _users.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<User> GetUserById(string id)
        {
            return await _users.Find(u => u.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreateUser(User user)
        {
            await _users.InsertOneAsync(user);
        }

        public async Task UpdateUser(User user)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Id, user.Id);
            await _users.ReplaceOneAsync(filter, user);
        }

        public async Task<bool> UserExists(string email)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Email, email);
            return await _users.Find(filter).AnyAsync();
        }
    }
}