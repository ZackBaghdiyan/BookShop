using BookShop.Data.Abstractions;

namespace BookShop.Data.Entities;

public class WishListItem : IIdentifiable
{
    public long Id { get; set; }
    public long ProductId { get; set; }
    public long WishListId { get; set; }
    public ClientEntity? Client { get; set; }
    public ProductEntity? Product { get; set; }
    public WishListEntity? WishList { get; set; }
}

