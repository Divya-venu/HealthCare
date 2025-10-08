using HealthCareApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HealthCareApp.Services.Interfaces
{
    public interface IMedicalContentService
    {
        Task<IEnumerable<MedicalContent>> GetAllMedicalContentAsync();
        Task<MedicalContent> CreateMedicalContentAsync(MedicalContent content);
    }
}