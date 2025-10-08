using System;
using System.ComponentModel.DataAnnotations;

namespace HealthCareApp.DTOs.Campaign
{
    public class CampaignDto
    {
        public string Name { get; set; }
        public string Therapy { get; set; }
        public string City { get; set; }
        public int Reach { get; set; }
        public int Shares { get; set; }
        public string Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ContentPack { get; set; }
        public string TargetAudience { get; set; }
        public string Language { get; set; }
    }
}