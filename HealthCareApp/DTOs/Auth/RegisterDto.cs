using System.ComponentModel.DataAnnotations;

namespace HealthCareApp.DTOs.Auth
{
    public class RegisterDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [Required]
        public string Role { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}