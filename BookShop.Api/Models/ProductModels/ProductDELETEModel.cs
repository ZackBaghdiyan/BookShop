using BookShop.Data.Abstractions;

namespace BookShop.Api.Models.ProductModels;

public class ProductDELETEModel : IIdentifiable
{
    public long Id { get; set; }
}
