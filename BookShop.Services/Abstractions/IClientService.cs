using BookShop.Data.Entities;
using BookShop.Services.Models.ClientModels;

namespace BookShop.Services.Abstractions;

public interface IClientService
{
    Task<(ClientGetVm, CartEntity, WishListEntity)> RegisterAsync(ClientRegisterVm entity);
    Task RemoveAsync(long clientId);
    Task<ClientGetVm> UpdateAsync(ClientUpdateVm entity);
    Task<ClientGetVm> GetByIdAsync(long entityId);
}
