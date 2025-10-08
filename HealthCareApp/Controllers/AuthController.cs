using HealthCareApp.DTOs.Auth;
using HealthCareApp.DTOs;
using HealthCareApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace HealthCareApp.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                var (user, token) = await _authService.RegisterAsync(registerDto);

                return StatusCode(StatusCodes.Status201Created, new ResponseDto<object>
                {
                    Status = "success",
                    Message = "User registered successfully",
                    Data = new { user, token }
                });
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new ResponseDto<object> { Status = "error", Message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDto<object> { Status = "error", Message = "Registration failed" });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                var (user, token) = await _authService.LoginAsync(loginDto);

                return Ok(new ResponseDto<object>
                {
                    Status = "success",
                    Message = "Login successful",
                    Data = new { user, token }
                });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new ResponseDto<object> { Status = "error", Message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDto<object> { Status = "error", Message = "Login failed" });
            }
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> GetCurrentUser()
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                // For now, returning a mock object. In a real scenario, you'd fetch the user from a service.
                return Ok(new ResponseDto<object>
                {
                    Status = "success",
                    Data = new { userId }
                });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDto<object> { Status = "error", Message = "Failed to get user data" });
            }
        }
    }
}