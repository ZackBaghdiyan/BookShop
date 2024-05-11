using BookShop.Services.Models.ClientModels;

namespace BookShop.Services.Abstractions;

public interface ICustomAuthenticationService
{
    string GenerateToken(string clientEmail);
    Task<ClientTokenVm?> AuthenticateAsync(string email, string password);
    string GetClientEmailFromToken();
}
