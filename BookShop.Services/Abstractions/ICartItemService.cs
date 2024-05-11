using BookShop.Services.Models.CartItemModels;

namespace BookShop.Services.Abstractions;

public interface ICartItemService
{
    Task<CartItemGetVm> AddAsync(CartItemAddVm cartItem);
    Task RemoveAsync(long cartItemId);
    Task<CartItemGetVm> UpdateAsync(CartItemUpdateVm cartItem);
}
