using BookShop.Data.Entities;

namespace BookShop.Services.Abstractions;

public interface IClientService
{
    Task RegisterAsync(ClientEntity entity);
    Task RemoveAsync(ClientEntity entity);
    Task UpdateAsync(ClientEntity entity);
}
