using HealthCareApp.Models;
using System.ComponentModel.DataAnnotations;

namespace HealthCareApp.DTOs.Hcp
{
    public class HcpUpdateEngagementDto
    {
        [Required]
        public HcpTotalEngagement TotalEngagement { get; set; }
    }
}