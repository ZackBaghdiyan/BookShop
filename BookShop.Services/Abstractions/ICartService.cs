using BookShop.Services.Models.CartItemModels;

namespace BookShop.Services.Abstractions;

public interface ICartService
{
    Task<List<CartItemGetVm>> GetAllItemsAsync(long cartId);
    Task ClearAllItemsAsync(long cartId);
}
