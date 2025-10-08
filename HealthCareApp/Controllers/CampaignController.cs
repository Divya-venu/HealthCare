using HealthCareApp.DTOs;
using HealthCareApp.Models;
using HealthCareApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using System.Security.Claims; 
using HealthCareApp.DTOs.Campaign; 

namespace HealthCareApp.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/campaigns")]
    public class CampaignController : ControllerBase
    {
        private readonly ICampaignService _campaignService;

        public CampaignController(ICampaignService campaignService)
        {
            _campaignService = campaignService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCampaigns()
        {
            try
            {
                var campaigns = await _campaignService.GetAllCampaignsAsync();
                return Ok(new ResponseDto<object>
                {
                    Status = "success",
                    Data = campaigns
                });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDto<object>
                {
                    Status = "error",
                    Message = "Failed to fetch campaigns"
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCampaign([FromBody] CreateCampaignDto campaignDto)
        {
            try
            {
                // Get the authenticated user's ID from the JWT token
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                // Map the DTO to the Campaign model
                var campaign = new Campaign
                {
                    Name = campaignDto.Name,
                    Therapy = campaignDto.Therapy,
                    City = campaignDto.City,
                    Reach = campaignDto.Reach,
                    Shares = campaignDto.Shares,
                    Status = campaignDto.Status,
                    StartDate = campaignDto.StartDate,
                    EndDate = campaignDto.EndDate,
                    ContentPack = campaignDto.ContentPack,
                    TargetAudience = campaignDto.TargetAudience,
                    Language = campaignDto.Language,
                    Completions = new Campaign.CampaignCompletions { Count = 0, Rate = 0 },
                    CreatedBy = userId
                };

                var createdCampaign = await _campaignService.CreateCampaignAsync(campaign);
                return StatusCode(StatusCodes.Status201Created, new ResponseDto<object>
                {
                    Status = "success",
                    Data = createdCampaign
                });
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new ResponseDto<object> { Status = "error", Message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDto<object>
                {
                    Status = "error",
                    Message = "Failed to create campaign"
                });
            }
        }
    }
}