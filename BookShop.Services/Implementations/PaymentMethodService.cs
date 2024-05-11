using BookShop.Data;
using BookShop.Data.Entities;
using BookShop.Data.Models;
using BookShop.Services.Abstractions;
using BookShop.Services.Models.PaymentMethodModels;
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

    public async Task<PaymentMethodGetVm> AddAsync(PaymentMethodAddVm paymentMethodAddVm)
    {
        if (paymentMethodAddVm == null)
        {
            throw new Exception("There is nothing to add");
        }

        var paymentMethod = new PaymentMethodEntity()
        {
            ClientId = paymentMethodAddVm.ClientId,
            PaymentMethod = paymentMethodAddVm.PaymentMethod,
            Details = JsonConvert.SerializeObject(paymentMethodAddVm.Details)
        };

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

        _dbContext.PaymentMethods.Add(paymentMethod);
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation($"PaymentMethod with Id {paymentMethod.Id} added successfully");

        var paymentMethodGetVm = new PaymentMethodGetVm()
        {
            Id = paymentMethod.Id,
            ClientId = paymentMethod.ClientId,
            PaymentMethod = paymentMethod.PaymentMethod,
            Details = JsonConvert.DeserializeObject<CardDetails>(paymentMethod.Details)
        };

        return paymentMethodGetVm;
    }

    public async Task<List<PaymentMethodGetVm>> GetAllAsync(long clientId)
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

        var paymentMethodsGetVmList = new List<PaymentMethodGetVm>();
        var paymentMethodsDb = await _dbContext.PaymentMethods.ToListAsync();

        foreach (var paymentMethod in paymentMethodsDb)
        {
            var paymentMethodGetVm = new PaymentMethodGetVm()
            {
                Id = paymentMethod.Id,
                ClientId = paymentMethod.ClientId,
                PaymentMethod = paymentMethod.PaymentMethod,
                Details = JsonConvert.DeserializeObject<CardDetails>(paymentMethod.Details)
            };

            paymentMethodsGetVmList.Add(paymentMethodGetVm);
        }

        return paymentMethodsGetVmList;
    }

    public async Task RemoveAsync(long paymentMethodId)
    {
        var paymentMethod = await _dbContext.PaymentMethods.FirstOrDefaultAsync(pm => pm.Id == paymentMethodId);

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
}
