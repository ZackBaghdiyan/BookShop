using BookShop.Data.Entities;

namespace BookShop.Services.Abstractions;

public interface IWishListService
{
    Task CreateAsync(long clientId);
    Task<List<WishListItemEntity>> GetAllItemsAsync(long wishListId);
    Task ClearAllItemsAsync(long wishListId);
}
