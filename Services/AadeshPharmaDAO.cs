using AadeshPharmaWeb.Interface;
using AadeshPharmaWeb.Model;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using WebApi.Entities;

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

        public bool addMedicine(Medicines medicines)
        {
            try
            {
                medicines.medicineId=Guid.NewGuid().ToString();
                 _medicinesCollection.InsertOne(medicines);
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

        public bool upser(Medicines medicines)
        {
            var filter = Builders<Medicines>.Filter.Eq(u => u.medicineId, medicines.medicineId);

            if(medicines.medicineId=="-1") medicines.medicineId=Guid.NewGuid().ToString();
            var result =  _medicinesCollection.ReplaceOne(filter,medicines,new ReplaceOptions { IsUpsert = true });
            if(result.ModifiedCount>0) return true; return false;
        }

        public bool update(Medicines medicines)
        {
            var filter = Builders<Medicines>.Filter.Eq(u => u.medicineId, medicines.medicineId);
            var update = Builders<Medicines>.Update.Set(u => u.medicineName, medicines.medicineName)
                                                  .Set(u => u.medicineManufactureBy, medicines.medicineManufactureBy)
                                                  .Set(u => u.manufacturedIn, medicines.manufacturedIn)
                                                  .Set(u => u.expiryDate, medicines.expiryDate)
                                                  .Set(u => u.price, medicines.price)
                                                  .Set(u => u.countryOfOrigin, medicines.countryOfOrigin)
                                                  .Set(u => u.composition, medicines.composition)
                                                  .Set(u => u.tags, medicines.tags)
                                                  .Set(u => u.directionOfUse, medicines.directionOfUse)
                                                  .Set(u => u.routeOfAdministration, medicines.routeOfAdministration)
                                                  .Set(u => u.sideEffects, medicines.sideEffects)
                                                  .Set(u => u.medicineActivity, medicines.medicineActivity)
                                                  .Set(u => u.interactions, medicines.interactions)
                                                  .Set(u => u.precaustionAndWarning, medicines.precaustionAndWarning)
                                                  .Set(u => u.dosage, medicines.dosage)
                                                  .Set(u => u.storage, medicines.storage)
                                                  .Set(u => u.imagesUrl, medicines.imagesUrl)
                                                  .Set(u => u.quantity, medicines.quantity);
            var result = _medicinesCollection.UpdateOne(filter, update);
            if (result.MatchedCount > 0) return true; return false;



        }

        public bool deleteMedecine(string id)
        {
            var filter = Builders<Medicines>.Filter.Eq(u => u.medicineId, id);
            var deleteResult = _medicinesCollection.DeleteOne(filter);
            if(deleteResult.DeletedCount > 0) return true; return false;
        }


    }
}
