using BookShop.Data.Abstractions;

namespace BookShop.Api.Models.WishListItemModels;

public class WishListItemGetModel : IIdentifiable
{
    public long Id { get; set; }
    public long ProductId { get; set; }
    public long WishListId { get; set; }
}
