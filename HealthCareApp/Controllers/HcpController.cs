using HealthCareApp.DTOs;
using HealthCareApp.DTOs.Hcp;
using HealthCareApp.Models;
using HealthCareApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace HealthCareApp.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/hcp")]
    public class HcpController : ControllerBase
    {
        private readonly IHcpService _hcpService;

        public HcpController(IHcpService hcpService)
        {
            _hcpService = hcpService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllHCPs()
        {
            try
            {
                var hcps = await _hcpService.GetAllHCPsAsync();
                return Ok(new ResponseDto<object> { Status = "success", Data = hcps });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDto<object> { Status = "error", Message = "Failed to fetch HCP data" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateHCP([FromBody] Hcp hcp)
        {
            try
            {
                var createdHcp = await _hcpService.CreateHCPAsync(hcp);
                return StatusCode(StatusCodes.Status201Created, new ResponseDto<object> { Status = "success", Data = createdHcp });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDto<object> { Status = "error", Message = "Failed to create HCP record" });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetHCPById(string id)
        {
            try
            {
                var hcp = await _hcpService.GetHCPByIdAsync(id);
                if (hcp == null)
                {
                    return NotFound(new ResponseDto<object> { Status = "error", Message = "HCP not found" });
                }
                return Ok(new ResponseDto<object> { Status = "success", Data = hcp });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDto<object> { Status = "error", Message = "Failed to fetch HCP data" });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHCP(string id, [FromBody] Hcp hcp)
        {
            try
            {
                var updatedHcp = await _hcpService.UpdateHCPAsync(id, hcp);
                if (updatedHcp == null)
                {
                    return NotFound(new ResponseDto<object> { Status = "error", Message = "HCP not found" });
                }
                return Ok(new ResponseDto<object> { Status = "success", Data = updatedHcp });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDto<object> { Status = "error", Message = "Failed to update HCP record" });
            }
        }

        [HttpPut("{id}/engagement")]
        public async Task<IActionResult> UpdateHcpEngagement(string id, [FromBody] HcpUpdateEngagementDto engagementDto)
        {
            try
            {
                var hcp = await _hcpService.UpdateHcpEngagementAsync(id, engagementDto.TotalEngagement);
                if (hcp == null)
                {
                    return NotFound(new ResponseDto<object> { Status = "error", Message = "HCP not found" });
                }
                return Ok(new ResponseDto<object> { Status = "success", Data = hcp });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDto<object> { Status = "error", Message = "Failed to update HCP engagement" });
            }
        }

        [HttpGet("analytics/summary")]
        public async Task<IActionResult> GetHcpAnalyticsSummary()
        {
            try
            {
                var summary = await _hcpService.GetHcpAnalyticsSummaryAsync();
                return Ok(new ResponseDto<object> { Status = "success", Data = summary });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDto<object> { Status = "error", Message = "Failed to fetch HCP analytics" });
            }
        }

        [HttpGet("engagement/top")]
        public async Task<IActionResult> GetTopEngagedHCPs()
        {
            try
            {
                var topHCPs = await _hcpService.GetTopEngagedHCPsAsync();
                return Ok(new ResponseDto<object> { Status = "success", Data = new { topEngagedHCPs = topHCPs } });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDto<object> { Status = "error", Message = "Failed to fetch top engaged HCPs" });
            }
        }

        [HttpGet("engagement/least")]
        public async Task<IActionResult> GetLeastEngagedHCPs()
        {
            try
            {
                var leastHCPs = await _hcpService.GetLeastEngagedHCPsAsync();
                return Ok(new ResponseDto<object> { Status = "success", Data = new { leastEngagedHCPs = leastHCPs } });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDto<object> { Status = "error", Message = "Failed to fetch least engaged HCPs" });
            }
        }
    }
}