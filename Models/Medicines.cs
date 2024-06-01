using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace AadeshPharmaWeb.Model
{
    public class Medicines
    {
        [BsonId]
        public ObjectId _id { get; set; }
        public Medicines()
        {
            this._id = ObjectId.GenerateNewId();
        }
        public string medicineId { get; set; }
        public string medicineName { get; set; }
        public int quantity { get; set; }
        public string medicineManufactureBy { get; set; }
        public DateTime manufacturedIn { get; set; }
        public DateTime expiryDate { get; set; }
        public double price { get; set; }
        public Country countryOfOrigin { get; set; }
        public List<string> composition { get; set; }
        public List<string> tags { get; set; }
        public string directionOfUse { get; set; }
        public string routeOfAdministration { get; set; }
        public string sideEffects { get; set; }
        public string medicineActivity { get; set; }
        public string interactions { get; set; }
        public string precaustionAndWarning { get; set; }
        public string dosage { get; set; }
        public string storage { get; set; }
        public List<string> imagesUrl { get; set; }
        

    }

    public class Country
    {
        public string name { get; set; }
        public string code { get; set; }

    }
}
