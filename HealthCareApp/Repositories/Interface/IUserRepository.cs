using HealthCareApp.Models;
using System.Threading.Tasks;

namespace HealthCareApp.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserByEmailAndRole(string email, string role);
        Task<User> GetUserById(string id);
        Task CreateUser(User user);
        Task UpdateUser(User user);
        Task<bool> UserExists(string email);
    }
}