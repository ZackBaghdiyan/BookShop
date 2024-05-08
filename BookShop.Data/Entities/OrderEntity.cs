using BookShop.Data.Abstractions;

namespace BookShop.Data.Entities;

public class OrderEntity : IIdentifiable
{
    public long Id { get; set; }
    public long ClientId { get; set; }
    public long ProductId { get; set; }
    public int Count { get; set; }
    public decimal Amount { get; set; }
    public InvoiceEntity? Invoice { get; set; }
    public ProductEntity? Product { get; set; }
    public ClientEntity? Client { get; set; }
}