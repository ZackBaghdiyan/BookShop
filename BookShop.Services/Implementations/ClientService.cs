using BookShop.Data.Entities;
using BookShop.Data;
using BookShop.Services.Abstractions;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

internal class ClientService : IClientService
{
    private readonly BookShopDbContext _bookShopDbContext;
    private readonly ILogger<ClientService> _loggerService;

    public ClientService(BookShopDbContext bookShopDbContext, ILogger<ClientService> loggerService)
    {
        _bookShopDbContext = bookShopDbContext;
        _loggerService = loggerService;
    }

    /// <summary>
    /// Changing existing client with your input student
    /// </summary>
    /// <param name="client"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public async Task UpdateAsync(ClientEntity client)
    {
        try
        {
            var clientToUpdate = await _bookShopDbContext.Clients.FirstOrDefaultAsync(s => s.Id == client.Id);

            if (clientToUpdate is null)
            {
                throw new InvalidOperationException("Client not found");
            }

            clientToUpdate.FirstName = client.FirstName;
            clientToUpdate.LastName = client.LastName;
            clientToUpdate.Email = client.Email;
            clientToUpdate.Address = client.Address;
            clientToUpdate.Password = client.Password;

            await _bookShopDbContext.SaveChangesAsync();
            _loggerService.Log(LogLevel.Information, $"Client with Id {clientToUpdate.Id} modified successfully.");
        }
        catch (Exception ex)
        {
            _loggerService.Log(LogLevel.Error, $"Error: {ex.Message}");
            throw;
        }
    }

    public async Task RegisterAsync(ClientEntity client)
    {
        try
        {
            _bookShopDbContext.Clients.Add(client);
            await _bookShopDbContext.SaveChangesAsync();
            _loggerService.LogInformation($"Client with Id {client.Id} added successfully.");

        }
        catch (Exception ex)
        {
            _loggerService.LogError(ex, $"Error occurred while adding client.");
        }
    }

    public async Task RemoveAsync(ClientEntity client)
    {
        try
        {
            var clientToRemove = await _bookShopDbContext.Clients.FirstOrDefaultAsync(c => c.Email == client.Email
                                                                                      && c.Password == client.Password);
            if (clientToRemove is null)
            {
                throw new Exception("There is no matching Client");
            }

            _bookShopDbContext.Clients.Remove(clientToRemove);
            await _bookShopDbContext.SaveChangesAsync();
            _loggerService.Log(LogLevel.Information, $"Client with Id {clientToRemove.Id} removed successfully.");
        }
        catch (Exception ex)
        {
            _loggerService.Log(LogLevel.Error, $"Error: {ex.Message}");
            throw;
        }
    }
}
