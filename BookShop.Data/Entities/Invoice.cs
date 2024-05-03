using BookShop.Data.Abstractions;

namespace BookShop.Data.Entities;

public class Invoice : IIdentifiable
{
    public int Id { get; set; }
    public int ClientId { get; set; }
    public DateTime CreatedAt { get; set; }
    public decimal TotalPrice { get; set; }
    public string ItemInformation { get; set; } = null!;
    public string PriceInformation { get; set; } = null!;
    public bool IsPaid { get; set; }

    public List<Payment>? Payments { get; set; }
    public Order? Order { get; set; }
    public Client? Client { get; set; }
}
