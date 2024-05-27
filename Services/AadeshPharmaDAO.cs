using AadeshPharmaWeb.Interface;
using AadeshPharmaWeb.Model;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace AadeshPharmaWeb.DAO
{
    public class AadeshPharmaDAO:IAadeshPharma
    {
        private readonly IMongoCollection<Medicines> _medicinesCollection;

        public AadeshPharmaDAO(IConfiguration configuration, IOptions<AadeshPharmaDatabaseConfiguration> database)
        {
            var mongoClient = new MongoClient(database.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(database.Value.DatabaseName);
            _medicinesCollection = mongoDatabase.GetCollection<Medicines>(database.Value.AadeshPharmaCollection);
        }

        public async Task<bool> addMedicine(Medicines medicines)
        {
            try
            {
                await _medicinesCollection.InsertOneAsync(medicines);
                return true;
            }
            catch { throw; }
        }

        public async Task<Medicines> getMedicineById(string id)
        {
            try
            {
                var medine = await _medicinesCollection.Find(x => (x.medicineId == id)).FirstOrDefaultAsync();
                return medine;
            }
            catch { throw; }
        }

        public async Task<List<Medicines>> getAllMedicines()
        {
            try
            {
                var medicines = _medicinesCollection.Find(Builders<Medicines>.Filter.Empty).ToList();
                return medicines;

            }
            catch { throw; }
        }

        public async Task<List<Medicines>> searchMedicines(string name)
        {
            try
            {
                var filter = Builders<Medicines>.Filter.Regex("medicineName", new BsonRegularExpression(name, "i")); // "i" for case insensitivity

                // Find documents matching the regex pattern
                var medicines = _medicinesCollection.Find(filter).ToList();

                return medicines;

            }
            catch { throw; }
        }
    }
}
