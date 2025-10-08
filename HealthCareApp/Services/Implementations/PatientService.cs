using HealthCareApp.Models;
using HealthCareApp.Repositories.Interfaces;
using HealthCareApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HealthCareApp.Services.Implementations
{
    public class PatientService : IPatientService
    {
        private readonly IPatientEngagementRepository _patientEngagementRepository;

        public PatientService(IPatientEngagementRepository patientEngagementRepository)
        {
            _patientEngagementRepository = patientEngagementRepository;
        }

        public async Task<IEnumerable<PatientEngagement>> GetAllPatientEngagementsAsync()
        {
            return await _patientEngagementRepository.GetAllPatientEngagementsAsync();
        }

        public async Task<PatientEngagement> CreatePatientEngagementAsync(PatientEngagement engagement)
        {
            await _patientEngagementRepository.CreatePatientEngagementAsync(engagement);
            return engagement;
        }

        public async Task<object> GetPatientEngagementAnalyticsAsync()
        {
            var totalEngagements = await _patientEngagementRepository.CountAllEngagementsAsync();
            var avgEngagementScore = await _patientEngagementRepository.GetAverageEngagementAsync();

            // Mock data for trends and distributions
            var topEngagementTypes = new[] { "Educational Content", "Health Tips", "Medication Reminders" };
            var demographicBreakdown = new Dictionary<string, int>
            {
                {"18-30", 25}, {"31-45", 35}, {"46-60", 28}, {"60+", 12}
            };
            var platformDistribution = new Dictionary<string, int>
            {
                {"Mobile App", 60}, {"Web Portal", 25}, {"SMS", 15}
            };

            return new
            {
                totalEngagements,
                avgEngagementScore,
                engagementTrend = "+8.3%",
                topEngagementTypes,
                demographicBreakdown,
                platformDistribution
            };
        }

        public async Task<object> GetPatientAnalyticsAsync()
        {
            var totalPatients = await _patientEngagementRepository.CountAllEngagementsAsync();
            // Note: The original JS code uses mock data for these metrics.
            // In a real app, these would come from the database or external services.
            var activePatients = totalPatients; // Assuming all are active for this mock

            return new
            {
                totalPatients,
                activePatients,
                engagementRate = 78.5,
                completionRate = 85.2,
                satisfactionScore = 4.3,
                retentionRate = 92.1
            };
        }
    }
}