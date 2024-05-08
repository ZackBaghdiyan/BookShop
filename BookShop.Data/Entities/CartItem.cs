using BookShop.Data.Abstractions;

namespace BookShop.Data.Entities;

public class CartItem : IIdentifiable
{
    public long Id { get; set; }
    public long ProductId { get; set; }
    public long CartId { get; set; }
    public long Count { get; set; }
    public decimal Price { get; set; }
    public ProductEntity? Product { get; set; }
    public CartEntity? Cart { get; set; }
}
