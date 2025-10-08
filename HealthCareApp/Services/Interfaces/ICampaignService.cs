using HealthCareApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HealthCareApp.Services.Interfaces
{
    public interface ICampaignService
    {
        Task<IEnumerable<Campaign>> GetAllCampaignsAsync();
        Task<Campaign> CreateCampaignAsync(Campaign campaign);
    }
}