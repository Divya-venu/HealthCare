using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HealthCareApp.Models
{
    public class MedicalContent
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } // Removed [Required]

        [Required]
        [EnumDataType(typeof(MedicalName))]
        public string MedicalName { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [EnumDataType(typeof(MedicalLanguage))]
        public string Language { get; set; }

        public List<ContentFile> ContentFiles { get; set; } = new List<ContentFile>();

        [Required]
        public DateTime ExpiryDate { get; set; }

        public string ReminderFrequency { get; set; } = "30d"; // Value should match enum string

        [EnumDataType(typeof(ApprovalStatus))]
        public string Status { get; set; } = "Pending";

        [BsonRepresentation(BsonType.ObjectId)]
        public string ApproverId { get; set; } // Removed [Required]

        public bool OnLabel { get; set; } = true;

        public string Notes { get; set; } // Removed [Required]

        public DateTime? ApprovalDate { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [Required] // This field is required from the authenticated user's ID
        public string CreatedBy { get; set; }

        public DateTime? LastReviewed { get; set; }
        public DateTime? ReviewReminder { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public void SetReviewReminder()
        {
            if (!string.IsNullOrEmpty(ReminderFrequency))
            {
                var reminderDays = int.Parse(ReminderFrequency.Replace("d", ""));
                ReviewReminder = ExpiryDate.AddDays(-reminderDays);
            }
        }
    }

    public class ContentFile
    {
        public string Filename { get; set; }
        public string OriginalName { get; set; }
        public string Path { get; set; }
        public long Size { get; set; }
        public string Mimetype { get; set; }
        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
    }

    public enum MedicalName
    {
        diabetes_care, foot_care, heart_health
    }

    public enum MedicalLanguage
    {
        english, hindi, tamil
    }

    public enum ReminderFrequency
    {
        d15,
        d30
    }

    public enum ApprovalStatus
    {
        Pending, Approved, Rejected
    }
}