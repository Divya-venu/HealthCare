using HealthCareApp.Models;
using HealthCareApp.DTOs.Auth;
using System.Threading.Tasks;

namespace HealthCareApp.Services.Interfaces
{
    public interface IAuthService
    {
        Task<(User user, string token)> RegisterAsync(RegisterDto registerDto);
        Task<(User user, string token)> LoginAsync(LoginDto loginDto);
    }
}