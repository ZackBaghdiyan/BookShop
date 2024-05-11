namespace BookShop.Services.Models.ProductModels;

public class ProductAddVm
{
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public string Manufacturer { get; set; } = null!;
    public string Details { get; set; } = null!;
    public int Count { get; set; }
}
