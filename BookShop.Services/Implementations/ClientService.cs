using BookShop.Data.Entities;
using BookShop.Data;
using Microsoft.Extensions.Logging;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using BookShop.Services.Abstractions;
using BookShop.Services.Models.ClientModels;
using AutoMapper;
using BookShop.Services.Exceptions;

internal class ClientService : IClientService
{
    private readonly BookShopDbContext _dbContext;
    private readonly ILogger<ClientService> _logger;
    private readonly ICustomAuthenticationService _customAuthenticationService;
    private readonly IMapper _mapper;

    public ClientService(BookShopDbContext bookShopDbContext, ILogger<ClientService> logger,
        ICustomAuthenticationService customAuthenticationService, IMapper mapper)
    {
        _dbContext = bookShopDbContext;
        _logger = logger;
        _customAuthenticationService = customAuthenticationService;
        _mapper = mapper;
    }

    public async Task<(ClientGetVm, CartEntity, WishListEntity)> RegisterAsync(ClientRegisterVm clientRegisterVm)
    {
        if (clientRegisterVm == null)
        {
            throw new Exception("There is nothing to create");
        }

        using (var transaction = _dbContext.Database.BeginTransaction())
        {
            try
            {
                var client = _mapper.Map<ClientEntity>(clientRegisterVm);
                client.Password = HashPassword(clientRegisterVm.Password);
                _dbContext.Clients.Add(client);

                _logger.LogInformation($"Client with Id {client.Id} added successfully.");

                var cart = new CartEntity { ClientId = client.Id };
                _dbContext.Carts.Add(cart);

                var wishList = new WishListEntity { ClientId = client.Id };
                _dbContext.WishLists.Add(wishList);

                _logger.LogInformation($"Cart and WishList created successfully for Client with Id {client.Id}");

                var clientGetVm = _mapper.Map<ClientGetVm>(client);
                await _dbContext.SaveChangesAsync();

                transaction.Commit();

                return (clientGetVm, cart, wishList);
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new TransactionFailedException("Transaction failed", ex);
            }
        }
    }

    public async Task<ClientGetVm> UpdateAsync(ClientUpdateVm clientUpdateVm)
    {
        var client = _mapper.Map<ClientEntity>(clientUpdateVm);

        var checkingClientEmail = _customAuthenticationService.GetClientEmailFromToken();

        var clientToUpdate = await _dbContext.Clients.FirstOrDefaultAsync(c => c.Id == client.Id);

        if (clientToUpdate is null)
        {
            throw new InvalidOperationException("Client not found");
        }

        if (clientToUpdate.Email != checkingClientEmail)
        {
            throw new InvalidOperationException("Unauthorized: You can only update your own client information");
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
        _logger.LogInformation($"Client with Id {client.Id} modified successfully.");

        var clientGetVm = _mapper.Map<ClientGetVm>(client);

        return clientGetVm;
    }

    public async Task RemoveAsync(long clientId)
    {
        var clientToRemove = await _dbContext.Clients.FirstOrDefaultAsync(c => c.Id == clientId);

        if (clientToRemove is null)
        {
            throw new Exception("There is no matching Client");
        }

        var checkingClientEmail = _customAuthenticationService.GetClientEmailFromToken();

        if (clientToRemove.Email != checkingClientEmail)
        {
            throw new InvalidOperationException("Unauthorized: You can only remove your own client.");
        }

        _dbContext.Clients.Remove(clientToRemove);
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation($"Client with Id {clientToRemove.Id} removed successfully.");
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

    public async Task<ClientGetVm> GetByIdAsync(long clientId)
    {
        var client = await _dbContext.Clients.FirstOrDefaultAsync(c => c.Id == clientId);

        if (client == null)
        {
            throw new Exception("Client not found");
        }

        var clientGetVm = _mapper.Map<ClientGetVm>(client);

        return clientGetVm;
    }
}