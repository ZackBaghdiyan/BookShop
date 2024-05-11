using BookShop.Services.Models.ProductModels;

namespace BookShop.Services.Abstractions;

public interface IProductService
{
    Task<ProductGetVm> AddAsync(ProductAddVm productEntity);
    Task RemoveAsync(long productId);
    Task<List<ProductGetVm>> GetAllAsync();
    Task<ProductGetVm> UpdateAsync(ProductUpdateVm productEntity);
    Task<ProductGetVm> GetByIdAsync(long productId);
    Task ClearAsync();
}
