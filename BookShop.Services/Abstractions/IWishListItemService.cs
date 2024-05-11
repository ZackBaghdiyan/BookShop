using BookShop.Services.Models.WishListItemModels;

namespace BookShop.Services.Abstractions;

public interface IWishListItemService
{
    Task<WishListItemGetVm> AddAsync(WishListItemAddVm wishListItem);
    Task RemoveAsync(long wishListItemId);
}
