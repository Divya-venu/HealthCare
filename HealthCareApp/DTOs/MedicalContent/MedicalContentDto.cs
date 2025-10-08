using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HealthCareApp.Models;

namespace HealthCareApp.DTOs.MedicalContent
{
    public class MedicalContentDto
    {
        public string MedicalName { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }
        public List<ContentFile> ContentFiles { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string ReminderFrequency { get; set; }
        public string Status { get; set; }
        public bool OnLabel { get; set; }
        public string Notes { get; set; }
    }
}