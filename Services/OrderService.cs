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
                order.events.Add(new Events());
                order.events[0].status = "Created";
                order.events[0].eventDate=DateTime.Now;

                order.events.Add(new Events());
                order.events[1].status = "Placed";
                order.events[1].eventDate = DateTime.Now;


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

        public long getOrderCount()
        {
            try
            {
                var res = _orderCollection.EstimatedDocumentCount();
                return res;
            }
            catch { throw; }
        }
        public double getTotalAmtofOrders()
        {
            try
            {
                double totalAmt = 0d;
                var res = getAllOrders();
                for (int i=0;i<res.Count; i++)
                {
                    totalAmt += res[i].totalAmount;
                }
                return totalAmt;
            }
            catch { throw; }
        }

        public List<Order> getAllOrders()
        {
            try
            {
                var res = _orderCollection.Find(Builders<Order>.Filter.Empty).ToList();
                return res;
            }
            catch { throw; }
        }
    }
}
