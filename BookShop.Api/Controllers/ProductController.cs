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
        return Ok();
    }

    [Authorize]
    [HttpPut]
    public async Task<ActionResult<ProductEntity>> UpdateProduct(ProductPutModel productInput)
    {
        var productToUpdate = _mapper.Map<ProductEntity>(productInput);
        await _productService.UpdateAsync(productToUpdate);

        return Ok();
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<ActionResult<ProductEntity>> RemoveProduct(long id)
    {
        await _productService.RemoveAsync(id);
        return Ok();
    }

    [Authorize]
    [HttpDelete]
    public async Task<ActionResult<ProductEntity>> ClearProducts()
    {
        await _productService.ClearAsync();
        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductGetModel>> GetProduct(long id)
    {
        var product = await _productService.GetByIdAsync(id);
        var productOutput = _mapper.Map<ProductGetModel>(product);
        return Ok(productOutput);
    }

    [HttpGet]
    public async Task<ActionResult<List<ProductGetModel>>> GetAllProducts()
    {
        var products = await _productService.GetAllAsync();
        var productsOutput = new List<ProductGetModel>();

        foreach (var product in products)
        {
            var productOutput = _mapper.Map<ProductGetModel>(product);
            productsOutput.Add(productOutput);
        }

        return Ok(productsOutput);
    }
}
