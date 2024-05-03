using BookShop.Data.Abstractions;

namespace BookShop.Data.Entities;

public class PaymentMethod : IIdentifiable
{
    public int Id { get; set; }
    public int ClientId { get; set; }
    public string WayToPay { get; set; } = null!;
    public string Details { get; set; } = null!;

    public Client? Client { get; set; }
    public List<Payment>? Payments { get; set; }
}
