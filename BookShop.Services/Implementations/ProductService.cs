using BookShop.Data;
using BookShop.Data.Entities;
using BookShop.Services.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BookShop.Services.Implementations;

internal class ProductService : IProductService
{
    private readonly BookShopDbContext _dbContext;
    private readonly ILogger<ProductService> _logger;

    public ProductService(BookShopDbContext bookShopDbContext, ILogger<ProductService> logger)
    {
        _dbContext = bookShopDbContext;
        _logger = logger;
    }

    public async Task AddAsync(ProductEntity productEntity)
    {
        try
        {
            productEntity.Details = SerializeDetails(productEntity.Details);

            _dbContext.Products.Add(productEntity);
            await _dbContext.SaveChangesAsync();
            _logger.LogInformation($"Product with Id {productEntity.Id} added successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error: {ex.Message}");
            throw;
        }
    }

    public async Task ClearAsync()
    {
        try
        {
            var products = await _dbContext.Products.ToListAsync();
            _dbContext.Products.RemoveRange(products);
            await _dbContext.SaveChangesAsync();
            _logger.LogInformation("All products cleared successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error: {ex.Message}");
            throw;
        }
    }

    public Task<List<ProductEntity>> GetAllAsync()
    {
        try
        {
            return _dbContext.Products.ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error: {ex.Message}");
            throw;
        }
    }

    public async Task<ProductEntity> GetByIdAsync(long productId)
    {
        try
        {
            var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == productId);

            if (product == null)
            {
                throw new Exception("Product not found");
            }

            return product;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error: {ex.Message}");
            throw;
        }
    }

    public async Task RemoveAsync(long productId)
    {
        try
        {
            var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == productId);

            if (product == null)
            {
                throw new Exception("Product not found");
            }

            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();
            _logger.LogInformation($"Product with Id {productId} removed successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error: {ex.Message}");
            throw;
        }
    }

    public async Task UpdateAsync(ProductEntity productEntity)
    {
        try
        {
            var productToUpdate = await GetByIdAsync(productEntity.Id);

            productEntity.Details = SerializeDetails(productEntity.Details);

            productToUpdate.Name = productEntity.Name;
            productToUpdate.Price = productEntity.Price;
            productToUpdate.Count = productEntity.Count;
            productToUpdate.Manufacturer = productEntity.Manufacturer;
            productToUpdate.Details = productEntity.Details;

            await _dbContext.SaveChangesAsync();
            _logger.LogInformation($"Product with Id {productEntity.Id} updated successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error: {ex.Message}");
            throw;
        }
    }

    private string SerializeDetails(string details)
    {
        return JsonConvert.SerializeObject(details);
    }
}
