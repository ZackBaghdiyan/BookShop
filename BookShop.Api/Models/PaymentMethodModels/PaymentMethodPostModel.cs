using BookShop.Data.Enums;

namespace BookShop.Api.Models.PaymentMethodModels;

public class PaymentMethodPostModel
{
    public long ClientId { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public string Details { get; set; } = null!;
}
