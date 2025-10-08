using HealthCareApp.Models;
using HealthCareApp.Repositories.Interfaces;
using HealthCareApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HealthCareApp.Services.Implementations
{
    public class MedicalContentService : IMedicalContentService
    {
        private readonly IMedicalContentRepository _medicalContentRepository;

        public MedicalContentService(IMedicalContentRepository medicalContentRepository)
        {
            _medicalContentRepository = medicalContentRepository;
        }

        public async Task<IEnumerable<MedicalContent>> GetAllMedicalContentAsync()
        {
            return await _medicalContentRepository.GetAllMedicalContentAsync();
        }

        public async Task<MedicalContent> CreateMedicalContentAsync(MedicalContent content)
        {
            // Apply business rules
            content.SetReviewReminder();

            await _medicalContentRepository.CreateMedicalContentAsync(content);
            return content;
        }
    }
}