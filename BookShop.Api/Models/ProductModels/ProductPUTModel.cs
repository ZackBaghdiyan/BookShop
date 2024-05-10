using BookShop.Data.Abstractions;

namespace BookShop.Api.Models.ProductModels;

public class ProductPutModel : IIdentifiable
{
    public long Id {  get; set; }
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public string Manufacturer { get; set; } = null!;
    public string Details { get; set; } = null!;
    public int Count { get; set; }
}
