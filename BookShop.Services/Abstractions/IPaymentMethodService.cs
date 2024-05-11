using BookShop.Services.Models.PaymentMethodModels;

namespace BookShop.Services.Abstractions;

public interface IPaymentMethodService
{
    Task<PaymentMethodGetVm> AddAsync(PaymentMethodAddVm paymentMethodEntity);
    Task RemoveAsync(long paymentMethodId);
    Task<List<PaymentMethodGetVm>> GetAllAsync(long clientId);
}
