    using AadeshPharmaWeb.Model;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using WebApi.Entities;

namespace WebApi.Models
{
    public class Order
    {
        [BsonId]
        public ObjectId _id { get; set; }
        public Order()
        {
            this._id = ObjectId.GenerateNewId();
        }
        public string orderId { get; set; }
        public User customer { get; set; }
        public Address orderAddress { get; set; }
        public List<Medicines> items { get; set; }
        public double totalAmount { get; set; }
        public DateTime orderDate { get; set; }
        public DateTime? delieveryDate { get; set; }
        public List<Events> events { get; set; }
        public string modeOfPayment { get; set; }
    }

    public class Events
    {
        public string status { get; set; } //Created,Placed, Shipped, Delivered, Cancelled
        public DateTime eventDate { get; set; }   

    }

}
