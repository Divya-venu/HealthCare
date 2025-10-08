using System.ComponentModel.DataAnnotations;
using System;
using HealthCareApp.Models;

namespace HealthCareApp.DTOs.Campaign
{
    public class CreateCampaignDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Therapy { get; set; }

        [Required]
        public string City { get; set; }

        public int Reach { get; set; }
        public int Shares { get; set; }
        public string Status { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public string ContentPack { get; set; }
        public string TargetAudience { get; set; }
        public string Language { get; set; }
    }
}