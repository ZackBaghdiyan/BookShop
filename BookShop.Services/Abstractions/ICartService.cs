using BookShop.Services.Models.CartItemModels;

namespace BookShop.Services.Abstractions;

public interface ICartService
{
    Task<List<CartItemModel>> GetAllItemsAsync();
    Task ClearAllItemsAsync();
}
