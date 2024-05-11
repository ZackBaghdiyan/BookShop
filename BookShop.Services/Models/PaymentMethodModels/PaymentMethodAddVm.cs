using BookShop.Data.Enums;
using BookShop.Data.Models;

namespace BookShop.Services.Models.PaymentMethodModels;

public class PaymentMethodAddVm
{
    public long ClientId { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public CardDetails? Details { get; set; } 
}
