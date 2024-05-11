using BookShop.Services.Models.WishListItemModels;

namespace BookShop.Services.Abstractions;

public interface IWishListService
{
    Task<List<WishListItemGetVm>> GetAllItemsAsync(long wishListId);
    Task ClearAllItemsAsync(long wishListId);
}
