using HealthCareApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HealthCareApp.Repositories.Interfaces
{
    public interface IMedicalContentRepository
    {
        Task<IEnumerable<MedicalContent>> GetAllMedicalContentAsync();
        Task<MedicalContent> GetMedicalContentByIdAsync(string id);
        Task CreateMedicalContentAsync(MedicalContent content);
        Task UpdateMedicalContentAsync(MedicalContent content);
    }
}