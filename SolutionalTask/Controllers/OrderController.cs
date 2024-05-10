using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SolutionalTask.DTO;
using SolutionalTask.Interfaces;
using SolutionalTask.Models;
using SolutionalTask.Repository;

namespace SolutionalTask.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        public OrderController(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<Order>))]
        public IActionResult GetOrders()
        {
            var orders = _mapper.Map<List<OrderDTO>>(_orderRepository.GetOrders());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(orders);
        }

        [HttpGet("{orderId}")]
        [ProducesResponseType(200, Type = typeof(Order))]
        [ProducesResponseType(400)]
        public IActionResult GetOrder(int id)
        {
            if (!_orderRepository.OrderExists(id))
            {
                return NotFound();
            }

            var order = _mapper.Map<OrderDTO>(_orderRepository.GetOrder(id));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(order);
        }

        [HttpGet("{orderId}")]
        [ProducesResponseType(200, Type = typeof(Order))]
        [ProducesResponseType(400)]
        public IActionResult GetOrderProducts(int id)
        {
            if (!_orderRepository.OrderExists(id))
            {
                return NotFound();
            }

            var products = _mapper.Map<List<ProductDTO>>(_orderRepository.GetOrderProducts(id));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(products);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateOrder([FromBody] Order order)
        {
            if (order == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var orderMap = _mapper.Map<Order>(order);

            if (!_orderRepository.CreateOrder(order))
            {
                ModelState.AddModelError("", "Error while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully saved");
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult AddProducts(int id, [FromBody] List<Product> products)
        {
            if (products == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var productsMap = _mapper.Map<List<Product>>(products);

            if (!_orderRepository.AddProducts(id, productsMap))
            {
                ModelState.AddModelError("", "Error while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully saved");
        }

        [HttpPatch("{orderId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult UpdateOrder([FromBody] int id)
        {
            if (!_orderRepository.OrderExists(id))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var order = _orderRepository.GetOrder(id);
            var orderMap = _mapper.Map<Order>(order);

            if (!_orderRepository.UpdateOrder(orderMap))
            {
                ModelState.AddModelError("", "Error while updating order");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully updated order");
        }

        [HttpPatch("{orderId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult UpdateProductQuantity(int orderId, int productId, int quantity)
        {
            if (!_orderRepository.OrderExists(orderId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_orderRepository.UpdateProductQuantity(orderId, productId, quantity))
            {
                ModelState.AddModelError("", "Error while updating order");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully updated order");
        }

        [HttpPatch("{orderId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult AddReplacementProduct(int orderId, int productId, [FromBody] Product product)
        {
            if (!_orderRepository.OrderExists(orderId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (product == null)
            {
                return BadRequest(ModelState);
            }

            if (!_orderRepository.AddReplacementProduct(orderId, productId, product))
            {
                ModelState.AddModelError("", "Error while updating order");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully updated order");
        }
    }
}
