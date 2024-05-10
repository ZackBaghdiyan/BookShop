using BookShop.Data.Entities;

namespace BookShop.Services.Abstractions;

public interface ICartService
{
    Task CreateAsync(long clientId);
    Task AddItemAsync(CartItemEntity cartItem);
    Task RemoveItemAsync(CartItemEntity cartItem);
    Task<List<CartItemEntity>> GetAllItemsAsync(long cartId);
}
