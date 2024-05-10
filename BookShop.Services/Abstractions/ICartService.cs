using BookShop.Data.Entities;

namespace BookShop.Services.Abstractions;

public interface ICartService
{
    Task CreateAsync(long clientId);
    Task<List<CartItemEntity>> GetAllItemsAsync(long cartId);
    Task ClearAllItemsAsync(long cartId);
}
