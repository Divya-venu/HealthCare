using HealthCareApp.Models;
using HealthCareApp.Repositories.Interfaces;
using HealthCareApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthCareApp.Services.Implementations
{
    public class HcpService : IHcpService
    {
        private readonly IHcpRepository _hcpRepository;

        public HcpService(IHcpRepository hcpRepository)
        {
            _hcpRepository = hcpRepository;
        }

        public async Task<IEnumerable<Hcp>> GetAllHCPsAsync()
        {
            return await _hcpRepository.GetAllHCPsAsync();
        }

        public async Task<Hcp> CreateHCPAsync(Hcp hcp)
        {
            await _hcpRepository.CreateHCPAsync(hcp);
            return hcp;
        }

        public async Task<Hcp> GetHCPByIdAsync(string id)
        {
            return await _hcpRepository.GetHCPByIdAsync(id);
        }

        public async Task<Hcp> UpdateHCPAsync(string id, Hcp updatedHcp)
        {
            var hcp = await _hcpRepository.GetHCPByIdAsync(id);
            if (hcp == null)
            {
                return null;
            }

            // Update only allowed fields
            hcp.Name = updatedHcp.Name;
            hcp.Specialty = updatedHcp.Specialty;
            hcp.City = updatedHcp.City;
            hcp.Email = updatedHcp.Email;
            hcp.Phone = updatedHcp.Phone;
            hcp.Shares = updatedHcp.Shares;
            hcp.IsActive = updatedHcp.IsActive;

            await _hcpRepository.UpdateHCPAsync(hcp);
            return hcp;
        }

        public async Task<Hcp> UpdateHcpEngagementAsync(string id, HcpTotalEngagement totalEngagement)
        {
            var hcp = await _hcpRepository.GetHCPByIdAsync(id);
            if (hcp == null)
            {
                return null;
            }

            hcp.TotalEngagement = totalEngagement;
            hcp.CalculateEngagementScore();
            hcp.LastActive = DateTime.UtcNow;

            await _hcpRepository.UpdateHCPAsync(hcp);
            return hcp;
        }

        public async Task<object> GetHcpAnalyticsSummaryAsync()
        {
            // Aggregate data from repository methods
            var totalHCPs = await _hcpRepository.CountAllHCPsAsync();
            var activeHCPs = await _hcpRepository.CountActiveHCPsAsync();
            var avgEngagement = await _hcpRepository.GetAverageEngagementScoreAsync();
            var weeklyActiveUsers = await _hcpRepository.CountWeeklyActiveHCPsAsync();
            var monthlyActiveUsers = await _hcpRepository.CountMonthlyActiveHCPsAsync();

            // Mock data for trends and distributions
            var topSpecialties = new[] { "Cardiology", "Neurology", "Oncology" };
            var regionalDistribution = new Dictionary<string, int>
            {
                {"North", 35}, {"South", 28}, {"East", 22}, {"West", 15}
            };

            return new
            {
                summary = new { totalHCPs, avgEngagementScore = avgEngagement },
                weeklyActiveUsers,
                monthlyActiveUsers,
                activeHCPs,
                engagementTrend = "+12.5%",
                topSpecialties,
                regionalDistribution
            };
        }

        public async Task<IEnumerable<Hcp>> GetTopEngagedHCPsAsync()
        {
            return await _hcpRepository.GetTopEngagedHCPsAsync();
        }

        public async Task<IEnumerable<Hcp>> GetLeastEngagedHCPsAsync()
        {
            return await _hcpRepository.GetLeastEngagedHCPsAsync();
        }
    }
}