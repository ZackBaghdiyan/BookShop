using BookShop.Data.Abstractions;

namespace BookShop.Data.Entities;

public class Product : IIdentifiable
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Type { get; set; } = null!;
    public decimal Price { get; set; }
    public string Manufacturer { get; set; } = null!;
    public int Quantity { get; set; }
    public string Details { get; set; } = null!;

    public List<Order>? Orders { get; set; }
    public List<WishList>? WishLists { get; set; }
    public List<Cart>? Carts { get; set; }
}
