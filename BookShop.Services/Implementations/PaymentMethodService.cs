using BookShop.Common.ClientService.Abstractions;
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
    private readonly IClientContextReader _clientContextReader;

    public PaymentMethodService(BookShopDbContext bookShopDbContext,
        ILogger<PaymentMethodService> logger, IClientContextReader clientContextReader)
    {
        _dbContext = bookShopDbContext;
        _logger = logger;
        _clientContextReader = clientContextReader;
    }

    public async Task<PaymentMethodModel> AddAsync(PaymentMethodAddModel paymentMethodAddModel)
    {
        var clientId = _clientContextReader.GetClientContextId();

        var paymentMethod = new PaymentMethodEntity()
        {
            ClientId = clientId,
            PaymentMethod = paymentMethodAddModel.PaymentMethod,
            Details = JsonConvert.SerializeObject(paymentMethodAddModel.Details)
        };

        _dbContext.PaymentMethods.Add(paymentMethod);
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation($"PaymentMethod with Id {paymentMethod.Id} added successfully");

        var paymentMethodModel = new PaymentMethodModel()
        {
            Id = paymentMethod.Id,
            PaymentMethod = paymentMethod.PaymentMethod,
            Details = JsonConvert.DeserializeObject<CardDetails>(paymentMethod.Details)
        };

        return paymentMethodModel;
    }

    public async Task<List<PaymentMethodModel>> GetAllAsync()
    {
        var clientId = _clientContextReader.GetClientContextId();

        var paymentMethodsDb = await _dbContext.PaymentMethods.Where(pm => pm.ClientId == clientId).ToListAsync();
        var paymentMethodsModelList = new List<PaymentMethodModel>();

        foreach (var paymentMethod in paymentMethodsDb)
        {
            var paymentMethodModel = new PaymentMethodModel()
            {
                Id = paymentMethod.Id,
                PaymentMethod = paymentMethod.PaymentMethod,
                Details = JsonConvert.DeserializeObject<CardDetails>(paymentMethod.Details)
            };

            paymentMethodsModelList.Add(paymentMethodModel);
        }

        return paymentMethodsModelList;
    }

    public async Task RemoveAsync(long paymentMethodId)
    {
        var clientId = _clientContextReader.GetClientContextId();

        var paymentMethod = await _dbContext.PaymentMethods.FirstOrDefaultAsync(pm => pm.Id == paymentMethodId && pm.ClientId == clientId);

        if (paymentMethod == null)
        {
            throw new Exception("PaymentMethod not found");
        }

        _dbContext.PaymentMethods.Remove(paymentMethod);
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation($"PaymentMethod with Id {paymentMethod.Id} removed successfully");
    }

    public async Task ChoosePaymentMethodType()
    {

    }
}
