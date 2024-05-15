using BookShop.Services.Models.ClientModels;

namespace BookShop.Services.Abstractions;

public interface IClientService
{
    Task<ClientModel> RegisterAsync(ClientRegisterModel client);
    Task<ClientModel> UpdateAsync(ClientUpdateModel client);
    Task RemoveAsync();
    Task<ClientModel?> GetClientAsync();
    Task<ClientModel?> GetByEmailAndPasswordAsync(string email, string password);
}
