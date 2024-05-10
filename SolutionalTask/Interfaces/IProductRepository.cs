using SolutionalTask.Models;

namespace SolutionalTask.Interfaces
{
    public interface IProductRepository
    {
        List<Product> GetProducts();
        Product GetProduct(int id);
        bool ProductExists(int id);
    }
}
