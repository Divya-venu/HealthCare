using HealthCareApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HealthCareApp.Services.Interfaces
{
    public interface IPatientService
    {
        Task<IEnumerable<PatientEngagement>> GetAllPatientEngagementsAsync();
        Task<PatientEngagement> CreatePatientEngagementAsync(PatientEngagement engagement);
        Task<object> GetPatientEngagementAnalyticsAsync();
        Task<object> GetPatientAnalyticsAsync();
    }
}