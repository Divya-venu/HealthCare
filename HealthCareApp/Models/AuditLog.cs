using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace HealthCareApp.Models
{
    public class AuditLog
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [Required]
        public string ContentId { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [Required]
        public string ApproverId { get; set; }

        [Required]
        [EnumDataType(typeof(AuditAction))]
        public string Action { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        [Required]
        [EnumDataType(typeof(AuditStatus))]
        public string Status { get; set; }

        public string Notes { get; set; }
        public string PreviousStatus { get; set; }

        public BsonDocument Metadata { get; set; }
    }

    public enum AuditAction
    {
        Approved, Rejected, Created, Updated, Deleted
    }

    public enum AuditStatus
    {
        Active, Rejected, Pending
    }
}