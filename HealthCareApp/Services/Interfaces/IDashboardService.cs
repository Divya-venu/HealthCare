using System.Threading.Tasks;

namespace HealthCareApp.Services.Interfaces
{
    public interface IDashboardService
    {
        Task<object> GetDashboardMetricsAsync();
        Task<object> GetDashboardStatsAsync();
        Task<object> GetRecentActivitiesAsync();
        Task<object> GetRoiSignalsAsync();
    }
}