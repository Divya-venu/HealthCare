using HealthCareApp.Models;
using HealthCareApp.Repositories.Interfaces;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace HealthCareApp.Repositories.Implementations
{
    public class MedicalContentRepository : IMedicalContentRepository
    {
        private readonly IMongoCollection<MedicalContent> _medicalContent;

        public MedicalContentRepository(IMongoClient client, IConfiguration config)
        {
            var database = client.GetDatabase(config.GetValue<string>("MongoDb:DatabaseName"));
            _medicalContent = database.GetCollection<MedicalContent>("MedicalContent");
        }

        public async Task<IEnumerable<MedicalContent>> GetAllMedicalContentAsync()
        {
            return await _medicalContent.Find(content => true).ToListAsync();
        }

        public async Task<MedicalContent> GetMedicalContentByIdAsync(string id)
        {
            return await _medicalContent.Find(content => content.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreateMedicalContentAsync(MedicalContent content)
        {
            await _medicalContent.InsertOneAsync(content);
        }

        public async Task UpdateMedicalContentAsync(MedicalContent content)
        {
            var filter = Builders<MedicalContent>.Filter.Eq(c => c.Id, content.Id);
            await _medicalContent.ReplaceOneAsync(filter, content);
        }
    }
}