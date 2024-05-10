using SolutionalTask.Models;

namespace SolutionalTask.Interfaces
{
    public interface IOrderRepository
    {
        List<Order> GetOrders();
        Order GetOrder(int id);
        List<Product> GetOrderProducts(int id);
        bool CreateOrder(Order order);
        bool AddProducts(int id, List<Product> products);
        bool UpdateOrder(Order order);
        bool UpdateProductQuantity(int orderId, int productId, int quantity);
        bool AddReplacementProduct(int orderId, int productId, Product product);
        bool OrderExists(int id);
        bool Save();
    }
}
