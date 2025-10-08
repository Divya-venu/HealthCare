using HealthCareApp.DTOs;
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
    [Route("api/patients")]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPatientEngagements()
        {
            try
            {
                var patientEngagements = await _patientService.GetAllPatientEngagementsAsync();
                return Ok(new ResponseDto<object> { Status = "success", Data = patientEngagements });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDto<object> { Status = "error", Message = "Failed to fetch patient engagement data" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreatePatientEngagement([FromBody] PatientEngagement engagement)
        {
            try
            {
                var createdEngagement = await _patientService.CreatePatientEngagementAsync(engagement);
                return StatusCode(StatusCodes.Status201Created, new ResponseDto<object> { Status = "success", Data = createdEngagement });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDto<object> { Status = "error", Message = "Failed to create patient engagement record" });
            }
        }

        [HttpGet("engagement")]
        public async Task<IActionResult> GetPatientEngagementAnalytics()
        {
            try
            {
                var analytics = await _patientService.GetPatientEngagementAnalyticsAsync();
                return Ok(new ResponseDto<object> { Status = "success", Data = analytics });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDto<object> { Status = "error", Message = "Failed to fetch patient engagement analytics" });
            }
        }

        [HttpGet("analytics")]
        public async Task<IActionResult> GetPatientAnalytics()
        {
            try
            {
                var analytics = await _patientService.GetPatientAnalyticsAsync();
                return Ok(new ResponseDto<object> { Status = "success", Data = analytics });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDto<object> { Status = "error", Message = "Failed to fetch patient analytics" });
            }
        }
    }
}