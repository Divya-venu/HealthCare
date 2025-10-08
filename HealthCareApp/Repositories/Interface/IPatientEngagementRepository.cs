using HealthCareApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HealthCareApp.Repositories.Interfaces
{
    public interface IPatientEngagementRepository
    {
        Task<IEnumerable<PatientEngagement>> GetAllPatientEngagementsAsync();
        Task<PatientEngagement> GetPatientEngagementByIdAsync(string id);
        Task CreatePatientEngagementAsync(PatientEngagement engagement);
        Task UpdatePatientEngagementAsync(PatientEngagement engagement);
        Task<long> CountAllEngagementsAsync();
        Task<double> GetAverageEngagementAsync();
    }
}