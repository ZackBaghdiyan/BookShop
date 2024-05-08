using BookShop.Data;
using BookShop.Data.Entities;
using BookShop.Services.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BookShop.Services.Implementations;

internal class ClientService : IClientService
{
    private readonly BookShopDbContext _bookShopDbContext;
    private readonly ILogger<ClientService> _loggerService;

    public ClientService(BookShopDbContext bookShopDbContext, ILogger<ClientService> loggerService)
    {
        _bookShopDbContext = bookShopDbContext;
        _loggerService = loggerService;
    }

    public async Task AddAsync(ClientEntity client)
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
    public async Task ClearAsync()
    {
        try
        {
            var clients = await _bookShopDbContext.Clients.ToListAsync();
            _bookShopDbContext.Clients.RemoveRange(clients);
            await _bookShopDbContext.SaveChangesAsync();
            _loggerService.LogInformation("All clients cleared successfully.");
        }
        catch (Exception ex)
        {
            _loggerService.LogError(ex, "Error occurred while clearing clients.");
            throw;
        }
    }

    public Task<List<ClientEntity>> GetAllAsync()
    {
        try
        {
            return _bookShopDbContext.Clients.ToListAsync();
        }
        catch (Exception ex)
        {
            _loggerService.LogError(ex, "Error occurred while retrieving all clients.");
            throw;
        }
    }

    public async Task<ClientEntity> GetByIdAsync(long clientId)
    {
        try
        {
            var client = await _bookShopDbContext.Clients
                                .FirstOrDefaultAsync(s => s.Id == clientId);

            if (client == null)
            {
                throw new Exception($"Client not found");
            }

            return client;
        }
        catch (Exception ex)
        {
            _loggerService.LogError(ex, $"Error occurred while retrieving client with Id {clientId}.");
            throw;
        }
    }

    public async Task RemoveAsync(ClientEntity client)
    {
        try
        {
            var clientToRemove = await _bookShopDbContext.Clients.FirstOrDefaultAsync(s => s.FirstName == client.FirstName
                                                && s.LastName == client.LastName && s.Address == client.Address
                                                && s.Email == client.Email && s.Password == client.Password);
            if (clientToRemove is null)
            {
                throw new Exception("Client not found");
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

    /// <summary>
    /// Changing existing client with your input student
    /// </summary>
    /// <param name="client"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public async Task ModifyAsync(ClientEntity client)
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

    public async Task RemoveByIdAsync(long clientId)
    {
        try
        {
            var client = _bookShopDbContext.Clients.FirstOrDefault(s => s.Id == clientId);
            if (client is null)
            {
                throw new Exception($"Client not found");
            }
            _bookShopDbContext.Clients.Remove(client);
            await _bookShopDbContext.SaveChangesAsync();
            _loggerService.Log(LogLevel.Information, $"Client with Id {clientId} removed successfully.");
        }
        catch (Exception ex)
        {
            _loggerService.Log(LogLevel.Error, $"Error: {ex.Message}");
            throw;
        }
    }
}
