using HealthCareApp.Models;
using HealthCareApp.Repositories.Interfaces;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace HealthCareApp.Repositories.Implementations
{
    public class CampaignRepository : ICampaignRepository
    {
        private readonly IMongoCollection<Campaign> _campaigns;

        public CampaignRepository(IMongoClient client, IConfiguration config)
        {
            var database = client.GetDatabase(config.GetValue<string>("MongoDb:DatabaseName"));
            _campaigns = database.GetCollection<Campaign>("Campaigns");
        }

        public async Task<IEnumerable<Campaign>> GetAllCampaignsAsync()
        {
            return await _campaigns.Find(campaign => true).ToListAsync();
        }

        public async Task<Campaign> GetCampaignByIdAsync(string id)
        {
            return await _campaigns.Find(campaign => campaign.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreateCampaignAsync(Campaign campaign)
        {
            await _campaigns.InsertOneAsync(campaign);
        }

        public async Task UpdateCampaignAsync(Campaign campaign)
        {
            var filter = Builders<Campaign>.Filter.Eq(c => c.Id, campaign.Id);
            await _campaigns.ReplaceOneAsync(filter, campaign);
        }
    }
}