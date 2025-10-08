using HealthCareApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HealthCareApp.Repositories.Interfaces
{
    public interface IAuditLogRepository
    {
        Task CreateLogAsync(AuditLog log);
        Task<AuditLog> GetLogByIdAsync(string id);
        Task<IEnumerable<AuditLog>> GetAllLogsAsync();
        Task<IEnumerable<AuditLog>> GetLogsByContentIdAsync(string contentId);
    }
}