using AutoMapper;
using BookShop.Data;
using BookShop.Data.Entities;
using BookShop.Services.Abstractions;
using BookShop.Services.Models.ProductModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

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

    public async Task<ProductModel> AddAsync(ProductAddModel productAddModel)
    {
        if(productAddModel.Count <= 0)
        {
            throw new Exception("Product count can't be less than 0");
        }

        var productCheck = await _dbContext.Products.FirstOrDefaultAsync(p => p.Manufacturer == productAddModel.Manufacturer
        && p.Details == p.Details && p.Name == productAddModel.Name && p.Price == productAddModel.Price);

        var product = new ProductEntity();
        var productModel = new ProductModel();

        if (productCheck != null)
        {
            productCheck.Count += productAddModel.Count;

            await _dbContext.SaveChangesAsync();
            _logger.LogInformation($"Product with Id {productCheck.Id} added successfully");

            productModel = _mapper.Map<ProductModel>(productCheck);

            return productModel;
        }

        product = _mapper.Map<ProductEntity>(productAddModel);

        _dbContext.Products.Add(product);
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation($"Product with Id {product.Id} added successfully");

        productModel = _mapper.Map<ProductModel>(product);

        return productModel;
    }

    public async Task ClearAsync()
    {
        var products = await _dbContext.Products.ToListAsync();
        _dbContext.Products.RemoveRange(products);
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation("All products cleared successfully");
    }

    public async Task<List<ProductModel>> GetAllAsync()
    {
        var productListDb = await _dbContext.Products.ToListAsync();
        var productModelList = _mapper.Map<List<ProductModel>>(productListDb);

        return productModelList;
    }

    public async Task<ProductModel> GetByIdAsync(long productId)
    {
        var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == productId);

        if (product == null)
        {
            throw new Exception("Product not found");
        }

        var productModel = _mapper.Map<ProductModel>(product);

        return productModel;
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

    public async Task<ProductModel> UpdateAsync(ProductUpdateModel productUpdateModel)
    {
        if (productUpdateModel.Count <= 0)
        {
            throw new Exception("Product count can't be less than 0");
        }

        var productToUpdate = await GetByIdAsync(productUpdateModel.Id);

        productToUpdate.Name = productUpdateModel.Name;
        productToUpdate.Price = productUpdateModel.Price;
        productToUpdate.Count = productUpdateModel.Count;
        productToUpdate.Manufacturer = productUpdateModel.Manufacturer;
        productToUpdate.Details = productUpdateModel.Details;

        await _dbContext.SaveChangesAsync();
        _logger.LogInformation($"Product with Id {productToUpdate.Id} updated successfully");

        var productModel = _mapper.Map<ProductModel>(productToUpdate);

        return productModel;
    }
}
