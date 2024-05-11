using AutoMapper;
using BookShop.Data;
using BookShop.Data.Entities;
using BookShop.Services.Abstractions;
using BookShop.Services.Models.ProductModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BookShop.Services.Implementations;

internal class ProductService : IProductService
{
    private readonly BookShopDbContext _dbContext;
    private readonly ILogger<ProductService> _logger;
    private readonly IMapper _mapper;

    public ProductService(BookShopDbContext bookShopDbContext, ILogger<ProductService> logger, IMapper mapper)
    {
        _dbContext = bookShopDbContext;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<ProductGetVm> AddAsync(ProductAddVm productAddVm)
    {
        var product = _mapper.Map<ProductEntity>(productAddVm);
        product.Details = SerializeDetails(productAddVm.Details);

        _dbContext.Products.Add(product);
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation($"Product with Id {product.Id} added successfully");

        var productGetVm = _mapper.Map<ProductGetVm>(product);

        return productGetVm;
    }

    public async Task ClearAsync()
    {
        var products = await _dbContext.Products.ToListAsync();
        _dbContext.Products.RemoveRange(products);
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation("All products cleared successfully");
    }

    public async Task<List<ProductGetVm>> GetAllAsync()
    {
        var productGetVmList = new List<ProductGetVm>();
        var productListDb = await _dbContext.Products.ToListAsync();

        foreach(var product in productListDb)
        {
            var productGetVm = _mapper.Map<ProductGetVm>(product);
            productGetVmList.Add(productGetVm);
        }

        return productGetVmList;
    }

    public async Task<ProductGetVm> GetByIdAsync(long productId)
    {
        var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == productId);

        if (product == null)
        {
            throw new Exception("Product not found");
        }

        var productGetVm = _mapper.Map<ProductGetVm>(product);

        return productGetVm;
    }

    public async Task RemoveAsync(long productId)
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

    public async Task<ProductGetVm> UpdateAsync(ProductUpdateVm productUpdateVm)
    {
        if(productUpdateVm == null)
        {
            throw new Exception("Nothing to Update");
        }

        var productEntity = _mapper.Map<ProductEntity>(productUpdateVm);

        var productToUpdate = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == productEntity.Id);

        if(productToUpdate == null)
        {
            throw new Exception("Product not found");
        }

        productToUpdate.Name = productEntity.Name;
        productToUpdate.Price = productEntity.Price;
        productToUpdate.Count = productEntity.Count;
        productToUpdate.Manufacturer = productEntity.Manufacturer;
        productToUpdate.Details = SerializeDetails(productEntity.Details);

        await _dbContext.SaveChangesAsync();
        _logger.LogInformation($"Product with Id {productToUpdate.Id} updated successfully");

        var productGetVm = _mapper.Map<ProductGetVm>(productToUpdate);

        return productGetVm;
    }

    private string SerializeDetails(string details)
    {
        return JsonConvert.SerializeObject(details);
    }
}
