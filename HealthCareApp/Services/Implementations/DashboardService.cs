using HealthCareApp.Repositories.Interfaces;
using HealthCareApp.Services.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;
using HealthCareApp.Models;
using System.Collections.Generic;

namespace HealthCareApp.Services.Implementations
{
    public class DashboardService : IDashboardService
    {
        private readonly ICampaignRepository _campaignRepository;
        private readonly IMedicalContentRepository _medicalContentRepository;
        private readonly IPatientEngagementRepository _patientEngagementRepository;
        private readonly IHcpRepository _hcpRepository;

        public DashboardService(
            ICampaignRepository campaignRepository,
            IMedicalContentRepository medicalContentRepository,
            IPatientEngagementRepository patientEngagementRepository,
            IHcpRepository hcpRepository)
        {
            _campaignRepository = campaignRepository;
            _medicalContentRepository = medicalContentRepository;
            _patientEngagementRepository = patientEngagementRepository;
            _hcpRepository = hcpRepository;
        }

        public async Task<object> GetDashboardMetricsAsync()
        {
            var totalCampaigns = await _campaignRepository.GetAllCampaignsAsync();
            var totalMedicalContent = await _medicalContentRepository.GetAllMedicalContentAsync();
            var totalPatientEngagement = await _patientEngagementRepository.GetAllPatientEngagementsAsync();
            var totalHCPs = await _hcpRepository.GetAllHCPsAsync();

            // Mock data calculations based on the original JS code
            var totalPatientEngagementsCount = totalPatientEngagement.Count();
            var totalHcpCount = totalHCPs.Count();

            var random = new Random();

            return new
            {
                activeCampaigns = totalCampaigns.Count(c => c.Status == "Active"),
                totalReach = totalPatientEngagementsCount * 10,
                doctorShares = totalHcpCount,
                completionRate = Math.Floor((double)random.Next(70, 100)),
                roiImprovement = Math.Floor((double)random.Next(10, 30)),
                patientEngagement = totalPatientEngagementsCount,
                hcpSatisfaction = Math.Floor((double)random.Next(80, 100))
            };
        }

        public async Task<object> GetDashboardStatsAsync()
        {
            var totalCampaigns = await _campaignRepository.GetAllCampaignsAsync();
            var totalMedicalContent = await _medicalContentRepository.GetAllMedicalContentAsync();
            var totalPatientEngagement = await _patientEngagementRepository.GetAllPatientEngagementsAsync();
            var totalHCPs = await _hcpRepository.GetAllHCPsAsync();

            return new
            {
                totalCampaigns = totalCampaigns.Count(),
                totalMedicalContent = totalMedicalContent.Count(),
                totalPatientEngagement = totalPatientEngagement.Count(),
                totalHCPs = totalHCPs.Count()
            };
        }

        public async Task<object> GetRecentActivitiesAsync()
        {
            var recentCampaigns = (await _campaignRepository.GetAllCampaignsAsync())
                                     .OrderByDescending(c => c.CreatedAt)
                                     .Take(5);

            var recentMedicalContent = (await _medicalContentRepository.GetAllMedicalContentAsync())
                                         .OrderByDescending(mc => mc.CreatedAt)
                                         .Take(5);

            return new
            {
                recentCampaigns,
                recentMedicalContent
            };
        }

        public async Task<object> GetRoiSignalsAsync()
        {
            // The original JS had mock data here, so we will do the same.
            // In a real application, this would involve complex calculations.
            var roiSignals = new object[]
            {
                new { metric = "Total ROI", value = "245.6%", change = "+12.3% this month" },
                new { metric = "Click Through Rate", value = "3.2%", change = "+0.8% vs last month" },
                new { metric = "Conversion Rate", value = "8.7%", change = "+1.2% vs last month" },
                new { metric = "Cost Per Acquisition", value = "$45.30", change = "-$5.20 vs last month" },
                new { metric = "Return on Ad Spend", value = "4.2x", change = "+0.3x vs last month" },
                new { metric = "Patient Engagement", value = "87%", change = "+5% vs last month" }
            };

            return roiSignals;
        }
    }
}