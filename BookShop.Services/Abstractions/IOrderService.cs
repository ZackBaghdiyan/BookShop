using BookShop.Services.Models.OrderModels;

namespace BookShop.Services.Abstractions;

public interface IOrderService
{
    Task<OrderModel> AddAsync(OrderAddModel orderAddModel);
    Task RemoveAsync(long orderId);
    public Task ClearAsync();
}
