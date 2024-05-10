using BookShop.Data.Entities;

namespace BookShop.Services.Abstractions;

public interface IWishListItemService
{
    Task AddAsync(WishListItemEntity wishListItem);
    Task RemoveAsync(WishListItemEntity wishListItem);
}
