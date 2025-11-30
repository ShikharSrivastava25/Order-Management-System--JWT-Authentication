using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderManagementAPI.DTOs;
using OrderManagementAPI.Services;
using System.Security.Claims;

namespace OrderManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private string GetUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var userId = GetUserId();
            var orders = await _orderService.GetOrdersAsync(userId);
            return Ok(orders);
        }

        [Authorize]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOrder(int id)
        {
            var userId = GetUserId();
            var order = await _orderService.GetOrderAsync(id, userId);

            if (order == null)
                return NotFound();

            return Ok(order);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderCreateDto dto, IValidator<OrderCreateDto> validator)
        {
            var result = await validator.ValidateAsync(dto);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            var userId = GetUserId();
            var created = await _orderService.CreateOrderAsync(dto, userId);

            return CreatedAtAction(nameof(GetOrder), new { id = created.Id }, created);
        }

        [Authorize]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateOrder(int id, OrderUpdateDto dto, IValidator<OrderUpdateDto> validator)
        {
            var result = await validator.ValidateAsync(dto);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            var userId = GetUserId();
            var success = await _orderService.UpdateOrderAsync(id, dto, userId);

            if (!success)
                return NotFound();

            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var userId = GetUserId();
            var success = await _orderService.DeleteOrderAsync(id, userId);

            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
