using HealthCareApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HealthCareApp.Services.Interfaces
{
    public interface IHcpService
    {
        Task<IEnumerable<Hcp>> GetAllHCPsAsync();
        Task<Hcp> CreateHCPAsync(Hcp hcp);
        Task<Hcp> GetHCPByIdAsync(string id);
        Task<Hcp> UpdateHCPAsync(string id, Hcp updatedHcp);
        Task<Hcp> UpdateHcpEngagementAsync(string id, HcpTotalEngagement totalEngagement);
        Task<object> GetHcpAnalyticsSummaryAsync();
        Task<IEnumerable<Hcp>> GetTopEngagedHCPsAsync();
        Task<IEnumerable<Hcp>> GetLeastEngagedHCPsAsync();
    }
}