using AadeshPharmaWeb.Model;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using WebApi.Interface;
using WebApi.Models;

namespace WebApi.Services
{
    public class OrderService:IOrder
    {
        private readonly IMongoCollection<Order> _orderCollection;
        public OrderService(IConfiguration configuration, IOptions<AadeshPharmaDatabaseConfiguration> database)
        {
            var mongoClient = new MongoClient(database.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(database.Value.DatabaseName);
            _orderCollection = mongoDatabase.GetCollection<Order>(database.Value.OrderCollectionName);
        }

        public string createOrder(Order order)
        {
            try
            {
                order.orderId = Guid.NewGuid().ToString();
                order.orderDate = DateTime.Now;
                order.status = "Pending";
                _orderCollection.InsertOne(order);
                return order.orderId;
            }
            catch { throw; }
        }
        public List<Order> getOrdersByUser(string userId)
        {
            try
            {
                var filter = Builders<Order>.Filter.Eq(u => u.customer.Id, userId);
                var result = _orderCollection.Find(filter).ToList();
                var r=_orderCollection.Find(u=> u.customer.Id == userId).ToList();
                return r;
            }
            catch { throw; };
        }
    }
}
