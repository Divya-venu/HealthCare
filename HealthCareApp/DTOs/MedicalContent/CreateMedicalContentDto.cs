using System.ComponentModel.DataAnnotations;
using System;
using HealthCareApp.Models;
using System.Collections.Generic;

namespace HealthCareApp.DTOs.MedicalContent
{
    public class CreateMedicalContentDto
    {
        [Required]
        public string MedicalName { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Language { get; set; }

        public List<ContentFile>? ContentFiles { get; set; }

        [Required]
        public DateTime ExpiryDate { get; set; }

        public string? ReminderFrequency { get; set; }
        public bool OnLabel { get; set; }
        public string? Notes { get; set; }
    }
}