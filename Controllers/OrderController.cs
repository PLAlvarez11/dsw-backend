using Microsoft.AspNetCore.Mvc;
using MessageApi.Services;
using HelloApi.Models.DTOs;

namespace MessageApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _service;

        public OrderController(IOrderService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult<OrderReadDto>> CreateOrder(OrderCreateDto dto)
        {
            var order = await _service.CreateOrderAsync(dto);
            return Ok(order);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderReadDto>>> GetOrders()
        {
            var orders = await _service.GetAllOrdersAsync();
            return Ok(orders);
        }
    }
}
