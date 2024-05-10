using SolutionalTask.Models;

namespace SolutionalTask.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public List<Product> Products { get; set; }
    }
}
