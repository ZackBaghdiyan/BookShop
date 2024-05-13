using BookShop.Services.Models.PaymentMethodModels;

namespace BookShop.Services.Abstractions;

public interface IPaymentMethodService
{
    Task<PaymentMethodModel> AddAsync(PaymentMethodAddModel paymentMethodEntity);
    Task RemoveAsync(long paymentMethodId);
    Task<List<PaymentMethodModel>> GetAllAsync();
}
