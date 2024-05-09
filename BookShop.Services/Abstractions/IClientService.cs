using BookShop.Data.Entities;

namespace BookShop.Services.Abstractions;

public interface IClientService
{
    Task RegisterAsync(ClientEntity entity);
    Task RemoveAsync(long entityId);
    Task UpdateAsync(ClientEntity entity);
    Task<ClientEntity> GetByIdAsync(long entityId);
}
