using BookShop.Services.Models.WishListItemModels;

namespace BookShop.Services.Abstractions;

public interface IWishListItemService
{
    Task<WishListItemModel> AddAsync(WishListItemAddModel wishListItem);
    Task RemoveAsync(long wishListItemId);
}
