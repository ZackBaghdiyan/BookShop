using AutoMapper;
using BookShop.Api.Models.ProductModels;
using BookShop.Data.Entities;
using BookShop.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly IMapper _mapper;

    public ProductController(IProductService productService, IMapper mapper)
    {
        _productService = productService;
        _mapper = mapper;
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<ProductEntity>> AddProduct(ProductPostModel productInput)
    {
        var productToAdd = _mapper.Map<ProductEntity>(productInput);
        await _productService.AddAsync(productToAdd);
        return Ok(productToAdd);
    }

    [Authorize]
    [HttpPut]
    public async Task<ActionResult<ProductEntity>> UpdateProduct(ProductPutModel productInput)
    {
        var productToUpdate = _mapper.Map<ProductEntity>(productInput);
        productToUpdate = await _productService.UpdateAsync(productToUpdate); 
        return Ok(productToUpdate);
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<ActionResult> RemoveProduct(long productId)
    {
        await _productService.RemoveAsync(productId);
        return Ok();
    }

    [Authorize]
    [HttpDelete]
    public async Task<ActionResult> ClearProducts()
    {
        await _productService.ClearAsync();
        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductEntity>> GetProduct(long productId)
    {
        var product = await _productService.GetByIdAsync(productId);
        return Ok(product);
    }

    [HttpGet]
    public async Task<ActionResult<List<ProductEntity>>> GetAllProducts()
    {
        var products = await _productService.GetAllAsync();
        return Ok(products);
    }
}
