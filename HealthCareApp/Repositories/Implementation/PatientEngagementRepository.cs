using HealthCareApp.Models;
using HealthCareApp.Repositories.Interfaces;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;

namespace HealthCareApp.Repositories.Implementations
{
    public class PatientEngagementRepository : IPatientEngagementRepository
    {
        private readonly IMongoCollection<PatientEngagement> _patientEngagements;

        public PatientEngagementRepository(IMongoClient client, IConfiguration config)
        {
            var database = client.GetDatabase(config.GetValue<string>("MongoDb:DatabaseName"));
            _patientEngagements = database.GetCollection<PatientEngagement>("PatientEngagements");
        }

        public async Task<IEnumerable<PatientEngagement>> GetAllPatientEngagementsAsync()
        {
            return await _patientEngagements.Find(engagement => true).ToListAsync();
        }

        public async Task<PatientEngagement> GetPatientEngagementByIdAsync(string id)
        {
            return await _patientEngagements.Find(engagement => engagement.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreatePatientEngagementAsync(PatientEngagement engagement)
        {
            await _patientEngagements.InsertOneAsync(engagement);
        }

        public async Task UpdatePatientEngagementAsync(PatientEngagement engagement)
        {
            var filter = Builders<PatientEngagement>.Filter.Eq(e => e.Id, engagement.Id);
            await _patientEngagements.ReplaceOneAsync(filter, engagement);
        }

        public async Task<long> CountAllEngagementsAsync()
        {
            return await _patientEngagements.CountDocumentsAsync(e => true);
        }

        public async Task<double> GetAverageEngagementAsync()
        {
            var pipeline = new[]
            {
                new BsonDocument("$group", new BsonDocument
                {
                    { "_id", BsonNull.Value },
                    { "avgEngagement", new BsonDocument("$avg", "$Engagement") }
                })
            };

            var result = await _patientEngagements.Aggregate<BsonDocument>(pipeline).FirstOrDefaultAsync();
            return result?.GetValue("avgEngagement", 0.0).AsDouble ?? 0.0;
        }
    }
}