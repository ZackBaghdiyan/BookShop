using BookShop.Services.Models.WishListItemModels;

namespace BookShop.Services.Abstractions;

public interface IWishListService
{
    Task<List<WishListItemModel>> GetAllItemsAsync();
    Task ClearAllItemsAsync();
}
