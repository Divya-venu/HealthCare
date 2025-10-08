using HealthCareApp.Models;
using HealthCareApp.Repositories.Interfaces;
using HealthCareApp.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HealthCareApp.Services.Implementations
{
    public class CampaignService : ICampaignService
    {
        private readonly ICampaignRepository _campaignRepository;

        public CampaignService(ICampaignRepository campaignRepository)
        {
            _campaignRepository = campaignRepository;
        }

        public async Task<IEnumerable<Campaign>> GetAllCampaignsAsync()
        {
            return await _campaignRepository.GetAllCampaignsAsync();
        }

        public async Task<Campaign> CreateCampaignAsync(Campaign campaign)
        {
            // Here we can apply business rules before saving
            // e.g., check if start date is before end date
            if (campaign.StartDate > campaign.EndDate)
            {
                throw new ApplicationException("Start date cannot be after end date.");
            }

            // Set default status if not provided
            if (string.IsNullOrEmpty(campaign.Status))
            {
                campaign.Status = "Draft";
            }

            // Calculate completion rate before saving
            if (campaign.Reach > 0)
            {
                campaign.Completions.Rate = System.Math.Round(((double)campaign.Completions.Count / campaign.Reach) * 100);
            }

            await _campaignRepository.CreateCampaignAsync(campaign);
            return campaign;
        }
    }
}