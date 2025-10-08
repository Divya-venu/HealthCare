using HealthCareApp.DTOs;
using HealthCareApp.DTOs.MedicalContent;
using HealthCareApp.Models;
using HealthCareApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HealthCareApp.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/medical")]
    public class MedicalContentController : ControllerBase
    {
        private readonly IMedicalContentService _medicalContentService;

        public MedicalContentController(IMedicalContentService medicalContentService)
        {
            _medicalContentService = medicalContentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMedicalContent()
        {
            try
            {
                var medicalContent = await _medicalContentService.GetAllMedicalContentAsync();
                return Ok(new ResponseDto<object> { Status = "success", Data = medicalContent });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDto<object> { Status = "error", Message = "Failed to fetch medical content" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateMedicalContent([FromBody] CreateMedicalContentDto contentDto)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized(new ResponseDto<object> { Status = "error", Message = "User is not authenticated." });
                }

                var medicalContent = new MedicalContent
                {
                    MedicalName = contentDto.MedicalName,
                    Description = contentDto.Description,
                    Language = contentDto.Language,
                    ExpiryDate = contentDto.ExpiryDate,
                    ReminderFrequency = contentDto.ReminderFrequency,
                    Notes = contentDto.Notes,
                    CreatedBy = userId
                };

                var createdContent = await _medicalContentService.CreateMedicalContentAsync(medicalContent);
                return StatusCode(StatusCodes.Status201Created, new ResponseDto<object> { Status = "success", Data = createdContent });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDto<object> { Status = "error", Message = "Failed to create medical content" });
            }
        }
    }
}