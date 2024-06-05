using WebApi.Models;

namespace WebApi.Interface
{
    public interface IOrder
    {
        string createOrder(Order order);
        List<Order> getOrdersByUser(string userId);
        long getOrderCount();
        double getTotalAmtofOrders();
        List<Order> getAllOrders();
    }
}
