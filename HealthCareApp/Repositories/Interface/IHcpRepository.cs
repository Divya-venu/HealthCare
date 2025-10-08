using HealthCareApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HealthCareApp.Repositories.Interfaces
{
    public interface IHcpRepository
    {
        Task<IEnumerable<Hcp>> GetAllHCPsAsync();
        Task<Hcp> GetHCPByIdAsync(string id);
        Task CreateHCPAsync(Hcp hcp);
        Task UpdateHCPAsync(Hcp hcp);
        Task<long> CountAllHCPsAsync();
        Task<long> CountActiveHCPsAsync();
        Task<double> GetAverageEngagementScoreAsync();
        Task<IEnumerable<Hcp>> GetTopEngagedHCPsAsync(int limit = 10);
        Task<IEnumerable<Hcp>> GetLeastEngagedHCPsAsync(int limit = 10);
        Task<long> CountWeeklyActiveHCPsAsync();
        Task<long> CountMonthlyActiveHCPsAsync();
    }
}