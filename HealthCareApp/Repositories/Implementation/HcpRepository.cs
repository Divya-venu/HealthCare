using HealthCareApp.Models;
using HealthCareApp.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HealthCareApp.Repositories.Implementations
{
    public class HcpRepository : IHcpRepository
    {
        private readonly IMongoCollection<Hcp> _hcps;

        public HcpRepository(IMongoClient client, IConfiguration config)
        {
            var database = client.GetDatabase(config.GetValue<string>("MongoDb:DatabaseName"));
            _hcps = database.GetCollection<Hcp>("HCPs");
        }

        public async Task<IEnumerable<Hcp>> GetAllHCPsAsync()
        {
            return await _hcps.Find(hcp => true).ToListAsync();
        }

        public async Task<Hcp> GetHCPByIdAsync(string id)
        {
            return await _hcps.Find(hcp => hcp.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreateHCPAsync(Hcp hcp)
        {
            await _hcps.InsertOneAsync(hcp);
        }

        public async Task UpdateHCPAsync(Hcp hcp)
        {
            var filter = Builders<Hcp>.Filter.Eq(h => h.Id, hcp.Id);
            await _hcps.ReplaceOneAsync(filter, hcp);
        }

        public async Task<long> CountAllHCPsAsync()
        {
            return await _hcps.CountDocumentsAsync(hcp => true);
        }

        public async Task<long> CountActiveHCPsAsync()
        {
            return await _hcps.CountDocumentsAsync(hcp => hcp.IsActive);
        }

        public async Task<double> GetAverageEngagementScoreAsync()
        {
            var pipeline = new[]
            {
                new BsonDocument("$group", new BsonDocument
                {
                    { "_id", BsonNull.Value },
                    { "avgScore", new BsonDocument("$avg", "$EngagementScore") }
                })
            };

            var result = await _hcps.Aggregate<BsonDocument>(pipeline).FirstOrDefaultAsync();

            if (result == null || !result.Contains("avgScore"))
            {
                return 0;
            }

            return result["avgScore"].AsDouble;
        }

        public async Task<IEnumerable<Hcp>> GetTopEngagedHCPsAsync(int limit = 10)
        {
            return await _hcps.Find(hcp => true).SortByDescending(h => h.EngagementScore).Limit(limit).ToListAsync();
        }

        public async Task<IEnumerable<Hcp>> GetLeastEngagedHCPsAsync(int limit = 10)
        {
            return await _hcps.Find(hcp => true).SortBy(h => h.EngagementScore).Limit(limit).ToListAsync();
        }

        public async Task<long> CountWeeklyActiveHCPsAsync()
        {
            var oneWeekAgo = DateTime.UtcNow.AddDays(-7);
            return await _hcps.CountDocumentsAsync(hcp => hcp.LastActive >= oneWeekAgo);
        }

        public async Task<long> CountMonthlyActiveHCPsAsync()
        {
            var oneMonthAgo = DateTime.UtcNow.AddMonths(-1);
            return await _hcps.CountDocumentsAsync(hcp => hcp.LastActive >= oneMonthAgo);
        }
    }
}