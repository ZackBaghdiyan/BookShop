namespace BookShop.Api.Models.ProductModels;

public class ProductPostModel
{
    public string Name { get; set; } = null!;
    public decimal Price { get; set; } 
    public string Manufacturer { get; set; } = null!;
    public string Details { get; set; } = null!;
    public int Count { get; set; }
}
