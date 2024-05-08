using BookShop.Data.Abstractions;

namespace BookShop.Data.Entities;

public class WishListEntity : IIdentifiable
{
    public long Id { get; set; }
    public List<WishListItem> WishListItems { get; set; } = new();
    public long ClientId { get; set; }
    public ClientEntity? Client { get; set; }
}