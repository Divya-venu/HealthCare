using HealthCareApp.DTOs.Auth;
using HealthCareApp.Models;
using HealthCareApp.Repositories.Interfaces;
using HealthCareApp.Services.Interfaces;
using HealthCareApp.Utilities;
using System;
using System.Threading.Tasks;

namespace HealthCareApp.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtUtility _jwtUtility;

        public AuthService(IUserRepository userRepository, IJwtUtility jwtUtility)
        {
            _userRepository = userRepository;
            _jwtUtility = jwtUtility;
        }

        public async Task<(User user, string token)> RegisterAsync(RegisterDto registerDto)
        {
            if (await _userRepository.UserExists(registerDto.Email))
            {
                throw new ApplicationException("User already exists with this email");
            }

            var user = new User
            {
                Email = registerDto.Email,
                Role = registerDto.Role,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName
            };

            user.SetHashedPassword(registerDto.Password);

            await _userRepository.CreateUser(user);
            var token = _jwtUtility.GenerateToken(user.Id);

            return (user, token);
        }

        public async Task<(User user, string token)> LoginAsync(LoginDto loginDto)
        {
            var user = await _userRepository.GetUserByEmailAndRole(loginDto.Email, loginDto.Role);

            if (user == null || !user.IsActive || !user.ComparePassword(loginDto.Password))
            {
                throw new UnauthorizedAccessException("Invalid credentials or account deactivated");
            }

            user.LastLogin = DateTime.UtcNow;
            await _userRepository.UpdateUser(user);

            var token = _jwtUtility.GenerateToken(user.Id);
            return (user, token);
        }
    }
}