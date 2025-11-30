using OrderManagementAPI.DTOs;

namespace OrderManagementAPI.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderReadDto>> GetOrdersAsync(string userId);
        Task<OrderReadDto?> GetOrderAsync(int id, string userId);
        Task<OrderReadDto> CreateOrderAsync(OrderCreateDto dto, string userId);
        Task<bool> UpdateOrderAsync(int id, OrderUpdateDto dto, string userId);
        Task<bool> DeleteOrderAsync(int id, string userId);
    }
}
