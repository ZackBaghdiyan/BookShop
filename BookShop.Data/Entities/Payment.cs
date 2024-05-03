using BookShop.Data.Abstractions;
using BookShop.Data.Enums;

namespace BookShop.Data.Entities;

public class Payment : IIdentifiable
{
    public int Id { get; set; }
    public int PaymentMethodId { get; set; }
    public int InvoiceId { get; set; }
    public PaymentStatus Status { get; set; }
    public decimal Price { get; set; }

    public PaymentMethod? PaymentMethod { get; set; }
    public Invoice? Invoice { get; set; }
}
