using BookShop.Data.Abstractions;

namespace BookShop.Data.Entities;

public class ProductEntity : IIdentifiable
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public string Manufacturer { get; set; } = null!;
    public string Details { get; set; } = null!;
    public int Count { get; set; }
    public CartItem? CartItem { get; set; } 
    public List<OrderEntity> Orders { get; set; } = new();
    public List<WishListItem> WishLists { get; set; } = new();
}
