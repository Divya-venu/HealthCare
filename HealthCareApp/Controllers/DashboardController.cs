using HealthCareApp.DTOs;
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
    [Route("api/dashboard")]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet("metrics")]
        public async Task<IActionResult> GetDashboardMetrics()
        {
            try
            {
                var metrics = await _dashboardService.GetDashboardMetricsAsync();
                return Ok(new ResponseDto<object> { Status = "success", Data = metrics });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDto<object> { Status = "error", Message = "Failed to fetch dashboard metrics" });
            }
        }

        [HttpGet("stats")]
        public async Task<IActionResult> GetDashboardStats()
        {
            try
            {
                var stats = await _dashboardService.GetDashboardStatsAsync();
                return Ok(new ResponseDto<object> { Status = "success", Data = stats });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDto<object> { Status = "error", Message = "Failed to fetch dashboard statistics" });
            }
        }

        [HttpGet("recent")]
        public async Task<IActionResult> GetRecentActivities()
        {
            try
            {
                var recentActivities = await _dashboardService.GetRecentActivitiesAsync();
                return Ok(new ResponseDto<object> { Status = "success", Data = recentActivities });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDto<object> { Status = "error", Message = "Failed to fetch recent activities" });
            }
        }

        [HttpGet("roi-signals")]
        public async Task<IActionResult> GetRoiSignals()
        {
            try
            {
                var roiSignals = await _dashboardService.GetRoiSignalsAsync();
                return Ok(new ResponseDto<object> { Status = "success", Data = roiSignals });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDto<object> { Status = "error", Message = "Failed to fetch ROI signals" });
            }
        }
    }
}