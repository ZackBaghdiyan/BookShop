using BookShop.Data;
using BookShop.Data.Entities;
using BookShop.Services.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BookShop.Services.Implementations;

internal class PaymentMethodService : IPaymentMethodService
{
    private readonly BookShopDbContext _dbContext;
    private readonly ILogger<PaymentMethodService> _logger;
    private readonly ICustomAuthenticationService _customAuthenticationService;

    public PaymentMethodService(BookShopDbContext bookShopDbContext, ILogger<PaymentMethodService> logger,
        ICustomAuthenticationService customAuthenticationService)
    {
        _dbContext = bookShopDbContext;
        _logger = logger;
        _customAuthenticationService = customAuthenticationService;
    }

    public async Task AddAsync(PaymentMethodEntity paymentMethodEntity)
    {
        try
        {
            if (paymentMethodEntity == null)
            {
                throw new Exception("There is nothing to add");
            }

            var client = await _dbContext.Clients.FirstOrDefaultAsync(c => c.Id == paymentMethodEntity.ClientId);

            if (client == null)
            {
                throw new Exception("Client not found");
            }

            var checkingClientEmail = _customAuthenticationService.GetClientEmailFromToken();

            if (client.Email != checkingClientEmail)
            {
                throw new Exception("Unauthorized: You can't add PaymentMethod for other Client");
            }

            paymentMethodEntity.Details = SerializeDetails(paymentMethodEntity.Details);

            _dbContext.PaymentMethods.Add(paymentMethodEntity);
            await _dbContext.SaveChangesAsync();
            _logger.LogInformation($"PaymentMethod with Id {paymentMethodEntity.Id} added successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error: {ex.Message}");
            throw;
        }
    }

    public async Task<List<PaymentMethodEntity>> GetAllAsync(long clientId)
    {
        try
        {
            var client = await _dbContext.Clients.FirstOrDefaultAsync(c => c.Id == clientId);

            if (client == null)
            {
                throw new Exception("Client not found");
            }

            var checkingClientEmail = _customAuthenticationService.GetClientEmailFromToken();

            if (client.Email != checkingClientEmail)
            {
                throw new Exception("Unauthorized: You can't get PaymentMethods of other Client");
            }

            return await _dbContext.PaymentMethods.ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error: {ex.Message}");
            throw;
        }
    }

    public async Task RemoveAsync(PaymentMethodEntity paymentMethodEntity)
    {
        try
        {
            var paymentMethod = await _dbContext.PaymentMethods.FirstOrDefaultAsync(pm => pm.Id == paymentMethodEntity.Id);

            if (paymentMethod == null)
            {
                throw new Exception("PaymentMethod not found");
            }

            var client = await _dbContext.Clients.FirstOrDefaultAsync(c => c.Id == paymentMethod.ClientId);

            if (client == null)
            {
                throw new Exception("Client not found");
            }

            var checkingClientEmail = _customAuthenticationService.GetClientEmailFromToken();

            if (client.Email != checkingClientEmail)
            {
                throw new Exception("Unauthorized: You can't add PaymentMethod for other Client");
            }

            _dbContext.PaymentMethods.Remove(paymentMethod);
            await _dbContext.SaveChangesAsync();
            _logger.LogInformation($"PaymentMethod with Id {paymentMethod.Id} removed successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error: {ex.Message}");
            throw;
        }
    }

    private string SerializeDetails(string details)
    {
        return JsonConvert.SerializeObject(details);
    }
}
