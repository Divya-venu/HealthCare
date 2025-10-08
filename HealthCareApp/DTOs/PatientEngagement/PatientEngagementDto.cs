using System;
using System.ComponentModel.DataAnnotations;
using HealthCareApp.Models;

namespace HealthCareApp.DTOs.PatientEngagement
{
    public class PatientEngagementDto
    {
        public string Week { get; set; }
        public int Completions { get; set; }
        public int Change { get; set; }
        public string City { get; set; }
        public string Language { get; set; }
        public double Engagement { get; set; }
        public Asset Asset { get; set; }
        public string DwellTime { get; set; }
        public string Effectiveness { get; set; }
    }
}