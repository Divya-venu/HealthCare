using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System.Security.Cryptography;
using System.Text;

namespace HealthCareApp.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } = "marketing";
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime? LastLogin { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Hash password before saving
        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        // Compare password method
        public bool ComparePassword(string candidatePassword)
        {
            return BCrypt.Net.BCrypt.Verify(candidatePassword, Password);
        }

        public void SetHashedPassword(string password)
        {
            Password = HashPassword(password);
        }
    }
}