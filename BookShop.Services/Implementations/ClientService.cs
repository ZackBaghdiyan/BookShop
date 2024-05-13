using BookShop.Data.Entities;
using BookShop.Data;
using Microsoft.Extensions.Logging;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using BookShop.Services.Abstractions;
using BookShop.Services.Models.ClientModels;
using AutoMapper;
using BookShop.Common.ClientService;
using BookShop.Services.Exceptions;

internal class ClientService : IClientService
{
    private readonly BookShopDbContext _dbContext;
    private readonly ILogger<ClientService> _logger;
    private readonly IMapper _mapper;
    private readonly ClientContextReader _clientContextReader;

    public ClientService(BookShopDbContext bookShopDbContext, ILogger<ClientService> logger,
        IMapper mapper, ClientContextReader clientContextReader)
    {
        _dbContext = bookShopDbContext;
        _logger = logger;
        _mapper = mapper;
        _clientContextReader = clientContextReader;
    }

    public async Task<ClientModel> RegisterAsync(ClientRegisterModel clientRegisterModel)
    {
        using (var transaction = _dbContext.Database.BeginTransaction())
        {
            try
            {
                var client = _mapper.Map<ClientEntity>(clientRegisterModel);
                client.Password = HashPassword(clientRegisterModel.Password);

                _dbContext.Clients.Add(client);
                await _dbContext.SaveChangesAsync();

                var cart = new CartEntity { ClientId = client.Id };
                _dbContext.Carts.Add(cart);

                var wishList = new WishListEntity { ClientId = client.Id };
                _dbContext.WishLists.Add(wishList);

                await _dbContext.SaveChangesAsync();
                _logger.LogInformation($"Client with Id {client.Id} added successfully.");

                var clientModel = _mapper.Map<ClientModel>(client);

                transaction.Commit();

                return clientModel;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new TransactionFailedException("Transaction failed", ex);
            }
        }
    }

    public async Task<ClientModel> UpdateAsync(ClientUpdateModel client)
    {
        var clientId = _clientContextReader.GetClientContextId();

        var clientToUpdate = await _dbContext.Clients.FirstOrDefaultAsync(c => c.Id == clientId);

        if (clientToUpdate is null)
        {
            throw new InvalidOperationException("Client not found");
        }

        clientToUpdate.FirstName = client.FirstName;
        clientToUpdate.LastName = client.LastName;
        clientToUpdate.Email = client.Email;
        clientToUpdate.Address = client.Address;

        if (!string.IsNullOrEmpty(client.Password))
        {
            clientToUpdate.Password = HashPassword(client.Password);
        }

        await _dbContext.SaveChangesAsync();
        _logger.LogInformation($"Client with Id {clientId} modified successfully.");

        var clientModel = _mapper.Map<ClientModel>(clientToUpdate);

        return clientModel;
    }

    public async Task RemoveAsync()
    {
        var clientId = _clientContextReader.GetClientContextId();

        var clientToRemove = await _dbContext.Clients.FirstOrDefaultAsync(c => c.Id == clientId);

        if (clientToRemove is null)
        {
            throw new Exception("There is no matching Client");
        }

        _dbContext.Clients.Remove(clientToRemove);
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation($"Client with Id {clientToRemove.Id} removed successfully.");
    }

    public async Task<ClientModel?> GetClientAsync()
    {
        var clientId = _clientContextReader.GetClientContextId();

        var client = await _dbContext.Clients.FirstOrDefaultAsync(c => c.Id == clientId);

        var clientModel = _mapper.Map<ClientModel>(client);

        return clientModel;
    }

    public async Task<ClientModel?> GetByEmailAndPasswordAsync(string email, string password)
    {
        var client = await _dbContext.Clients.FirstOrDefaultAsync(c => c.Email == email);

        if (client != null)
        {
            var hashedPassword = HashPassword(password);
            if (client.Password == hashedPassword)
            {
                return _mapper.Map<ClientModel?>(client);
            }
        }

        return null;
    }

    private string HashPassword(string password)
    {
        var passwordBytes = Encoding.UTF8.GetBytes(password);
        var passwordHash = SHA256.HashData(passwordBytes);
        return Convert.ToHexString(passwordHash);
    }
}