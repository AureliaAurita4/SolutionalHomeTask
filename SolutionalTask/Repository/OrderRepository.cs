using SolutionalTask.Data;
using SolutionalTask.Interfaces;
using SolutionalTask.Models;

namespace SolutionalTask.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DataContext _context;
        public OrderRepository(DataContext context)
        {
            _context = context;
        }

        public List<Order> GetOrders()
        {
            return _context.Orders.OrderBy(i => i.Id).ToList();
        }

        public Order GetOrder(int id)
        {
            return _context.Orders.Where(o => o.Id == id).FirstOrDefault();
        }

        public List<Product> GetOrderProducts(int id)
        {
            return GetOrder(id).Products;
        }

        public bool CreateOrder(Order order)
        {
            _context.Add(order);
            return Save();
        }

        public bool AddProducts(int id, List<Product> products)
        {
            var order = GetOrder(id);

            foreach (var product in products)
            {
                _context.Add(product);
            }
            return Save();
        }

        public bool UpdateOrder(Order order)
        {
            _context.Update(order);
            return Save();
        }


        public bool UpdateProductQuantity(int orderId, int productId, int quantity)
        {
            var order = GetOrder(orderId);
            var product = order.Products.Where(p => p.Id == productId).FirstOrDefault();

            if (product != null)
            {
                product.Quantity = quantity;
                _context.Update(product);
            }
            
            return Save();
        }

        public bool AddReplacementProduct(int orderId, int productId, Product product)
        {
            var order = GetOrder(orderId);
            var orderProduct = order.Products.Where(p => p.Id == productId).FirstOrDefault();

            if (orderProduct != null)
            {
                orderProduct = product;
                _context.Update(orderProduct);
            }

            return Save();
        }

        public bool OrderExists(int id)
        {
            return _context.Orders.Any(o => o.Id == id);
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0 ? true : false;
        }
    }
}
