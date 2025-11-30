using Microsoft.EntityFrameworkCore;
using OrderManagementAPI.Data;
using OrderManagementAPI.DTOs;
using OrderManagementAPI.Models;

namespace OrderManagementAPI.Services
{
    public class OrderService : IOrderService
    {

        private readonly ApplicationDbContext _dbContext;
        public OrderService(ApplicationDbContext db)
        {
            _dbContext = db;
        }

        public async Task<OrderReadDto> CreateOrderAsync(OrderCreateDto dto, string userId)
        {
            var order = new Order
            {
                ProductName = dto.ProductName,
                Quantity = dto.Quantity,
                UnitPrice = dto.UnitPrice,
                TotalAmount = dto.Quantity * dto.UnitPrice,
                UserId = userId
            };

            _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync();

            return new OrderReadDto
            {
                Id = order.Id,
                ProductName = order.ProductName,
                Quantity = order.Quantity,
                UnitPrice = order.UnitPrice,
                TotalAmount = order.TotalAmount
            };
        }

        public async Task<IEnumerable<OrderReadDto>> GetOrdersAsync(string userId)
        {
            return await _dbContext.Orders
                .Where(o => o.UserId == userId)
                .Select(o => new OrderReadDto
                {
                    Id = o.Id,
                    ProductName = o.ProductName,
                    Quantity = o.Quantity,
                    UnitPrice = o.UnitPrice,
                    TotalAmount = o.TotalAmount
                })
                .ToListAsync();
        }

        public async Task<OrderReadDto?> GetOrderAsync(int id, string userId)
        {
            var order = await _dbContext.Orders.FirstOrDefaultAsync(o => o.Id == id && o.UserId == userId);

            if (order is null) return null;

            return new OrderReadDto
            {
                Id = order.Id,
                ProductName = order.ProductName,
                Quantity = order.Quantity,
                UnitPrice = order.UnitPrice,
                TotalAmount = order.TotalAmount
            };
        }
        public async Task<bool> UpdateOrderAsync(int id, OrderUpdateDto dto, string userId)
        {
            var order = await _dbContext.Orders
                .FirstOrDefaultAsync(o => o.Id == id && o.UserId == userId);

            if (order == null) return false;

            order.ProductName = dto.ProductName;
            order.Quantity = dto.Quantity;
            order.UnitPrice = dto.UnitPrice;
            order.TotalAmount = dto.Quantity * dto.UnitPrice;

            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteOrderAsync(int id, string userId)
        {
            var order = await _dbContext.Orders
                .FirstOrDefaultAsync(o => o.Id == id && o.UserId == userId);

            if (order == null) return false;

            _dbContext.Orders.Remove(order);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
