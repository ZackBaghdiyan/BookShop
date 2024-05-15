using BookShop.Services.Models.ProductModels;

namespace BookShop.Services.Abstractions;

public interface IProductService
{
    Task<ProductModel> AddAsync(ProductAddModel productEntity);
    Task RemoveAsync(long productId);
    Task<List<ProductModel>> GetAllAsync();
    Task<ProductModel> GetByIdAsync(long productId);
    Task<ProductModel> UpdateAsync(ProductUpdateModel productEntity);
    Task ClearAsync();
}
