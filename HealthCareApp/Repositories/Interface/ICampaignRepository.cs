using HealthCareApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HealthCareApp.Repositories.Interfaces
{
    public interface ICampaignRepository
    {
        Task<IEnumerable<Campaign>> GetAllCampaignsAsync();
        Task<Campaign> GetCampaignByIdAsync(string id);
        Task CreateCampaignAsync(Campaign campaign);
        Task UpdateCampaignAsync(Campaign campaign);
    }
}