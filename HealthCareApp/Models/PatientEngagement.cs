using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace HealthCareApp.Models
{
    public class PatientEngagement
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [Required]
        public string Week { get; set; }

        [Required]
        public int Completions { get; set; }

        public int Change { get; set; } = 0;

        [Required]
        public string City { get; set; }

        [Required]
        public PatientLanguage Language { get; set; } // Changed from string to enum

        public double Engagement { get; set; }

        [Required]
        public Asset Asset { get; set; }

        public string DwellTime { get; set; } = "0 min";

        public Effectiveness Effectiveness { get; set; } = Effectiveness.Medium; // Changed from string to enum

        [BsonRepresentation(BsonType.ObjectId)]
        public string CampaignId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }

    public class Asset
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public AssetType Type { get; set; } // Changed from string to enum

        [Required]
        public AssetLanguage Language { get; set; } // Changed from string to enum
    }

    public enum PatientLanguage
    {
        english, hindi, mixed
    }

    public enum AssetType
    {
        video, pdf, image
    }

    public enum AssetLanguage
    {
        english, hindi, mixed
    }

    public enum Effectiveness
    {
        High, Medium, Low
    }
}