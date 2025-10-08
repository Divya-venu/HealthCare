using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace HealthCareApp.Models
{
    public class Campaign
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } // Removed [Required]

        [Required]
        public string Name { get; set; }

        [Required]
        public string Therapy { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public int Reach { get; set; }

        public int Shares { get; set; } = 0;

        public CampaignCompletions Completions { get; set; } = new CampaignCompletions();

        [EnumDataType(typeof(CampaignStatus))]
        public string Status { get; set; } = "Draft";

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        [EnumDataType(typeof(ContentPack))]
        public string ContentPack { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string CreatedBy { get; set; } // Removed [Required]

        [EnumDataType(typeof(TargetAudience))]
        public string TargetAudience { get; set; } = "both";

        [EnumDataType(typeof(CampaignLanguage))]
        public string Language { get; set; } = "english";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public class CampaignCompletions
        {
            public int Count { get; set; } = 0;
            public double Rate { get; set; } = 0;
        }
    }
    public enum TherapyArea
    {
        diabetes, hypertension, cardiac, oncology
    }

    public enum CampaignStatus
    {
        Active, Paused, Completed, Draft
    }

    public enum ContentPack
    {
        video_en, video_hi, pdf_en, pdf_hi
    }

    public enum TargetAudience
    {
        patients, hcp, both
    }

    public enum CampaignLanguage
    {
        english, hindi, tamil, mixed
    }
}