using BookShop.Data.Entities;
using BookShop.Data;
using BookShop.Services.Abstractions;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

internal class ClientService : IClientService
{
    private readonly BookShopDbContext _bookShopDbContext;
    private readonly ILogger<ClientService> _loggerService;

    public ClientService(BookShopDbContext bookShopDbContext, ILogger<ClientService> loggerService)
    {
        _bookShopDbContext = bookShopDbContext;
        _loggerService = loggerService;
    }

    public async Task RegisterAsync(ClientEntity clientEntity)
    {
        try
        {
            clientEntity.Password = HashPassword(clientEntity.Password);

            _bookShopDbContext.Clients.Add(clientEntity);
            await _bookShopDbContext.SaveChangesAsync();
            _loggerService.LogInformation($"Client with Id {clientEntity.Id} added successfully.");

        }
        catch (Exception ex)
        {
            _loggerService.LogError(ex, $"Error occurred while adding client.");
            throw;
        }
    }

    /// <summary>
    /// Changing existing client with your input student
    /// </summary>
    /// <param name="clientEntity"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public async Task UpdateAsync(ClientEntity clientEntity)
    {
        try
        {
            var clientToUpdate = await _bookShopDbContext.Clients.FirstOrDefaultAsync(c => c.Email == clientEntity.Email);

            if (clientToUpdate is null)
            {
                throw new InvalidOperationException("Client not found");
            }

            clientToUpdate.FirstName = clientEntity.FirstName;
            clientToUpdate.LastName = clientEntity.LastName;
            clientToUpdate.Email = clientEntity.Email;
            clientToUpdate.Address = clientEntity.Address;

            if(!string.IsNullOrEmpty(clientEntity.Password))
            {
                clientToUpdate.Password = HashPassword(clientEntity.Password);
            }

            await _bookShopDbContext.SaveChangesAsync();
            _loggerService.Log(LogLevel.Information, $"Client with Id {clientEntity.Id} modified successfully.");
        }
        catch (Exception ex)
        {
            _loggerService.Log(LogLevel.Error, $"Error: {ex.Message}");
            throw;
        }
    }

    public async Task RemoveAsync(ClientEntity clientEntity)
    {
        try
        {
            var clientToRemove = await _bookShopDbContext.Clients.FirstOrDefaultAsync(c => c.Email == clientEntity.Email);

            if (clientToRemove is null)
            {
                throw new Exception("There is no matching Client");
            }

            clientEntity.Password = HashPassword(clientEntity.Password);

            if(clientEntity.Password != clientToRemove.Password)
            {
                throw new Exception("Invalid password");
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

    private string HashPassword(string password)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < hashedBytes.Length; i++)
            {
                builder.Append(hashedBytes[i].ToString("x2"));
            }

            return builder.ToString();
        }
    }
}
