using HealthCareApp.Models;
using HealthCareApp.Repositories.Interfaces;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace HealthCareApp.Repositories.Implementations
{
    public class AuditLogRepository : IAuditLogRepository
    {
        private readonly IMongoCollection<AuditLog> _auditLogs;

        public AuditLogRepository(IMongoClient client, IConfiguration config)
        {
            var database = client.GetDatabase(config.GetValue<string>("MongoDb:DatabaseName"));
            _auditLogs = database.GetCollection<AuditLog>("AuditLogs");
        }

        public async Task CreateLogAsync(AuditLog log)
        {
            await _auditLogs.InsertOneAsync(log);
        }

        public async Task<AuditLog> GetLogByIdAsync(string id)
        {
            return await _auditLogs.Find(log => log.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<AuditLog>> GetAllLogsAsync()
        {
            return await _auditLogs.Find(log => true).ToListAsync();
        }

        public async Task<IEnumerable<AuditLog>> GetLogsByContentIdAsync(string contentId)
        {
            return await _auditLogs.Find(log => log.ContentId == contentId).ToListAsync();
        }
    }
}