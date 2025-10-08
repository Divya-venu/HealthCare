using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HealthCareApp.Models
{
    public class Hcp
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EnumDataType(typeof(HcpSpecialty))]
        public string Specialty { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Email { get; set; }

        public string Phone { get; set; }

        public int Shares { get; set; } = 0;

        public double EngagementScore { get; set; } = 0;

        public DateTime LastActive { get; set; } = DateTime.UtcNow;

        public bool IsActive { get; set; } = true;

        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> Campaigns { get; set; } = new List<string>();

        public HcpTotalEngagement TotalEngagement { get; set; } = new HcpTotalEngagement();

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public void CalculateEngagementScore()
        {
            var total = TotalEngagement.Views + TotalEngagement.Shares + TotalEngagement.Completions;
            if (total > 0)
            {
                EngagementScore = Math.Round(((double)TotalEngagement.Shares * 3 + (double)TotalEngagement.Completions * 2 + (double)TotalEngagement.Views) / total * 10, 2);
            }
        }
    }

    public class HcpTotalEngagement
    {
        public int Views { get; set; } = 0;
        public int Shares { get; set; } = 0;
        public int Completions { get; set; } = 0;
    }

    public enum HcpSpecialty
    {
        Endocrinologist, Cardiologist, General_Physician, Diabetologist, Internal_Medicine
    }
}