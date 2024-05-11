using BookShop.Services.Models.ProductModels;
using BookShop.Data.Entities;
using BookShop.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Api.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpPost]
    public async Task<ActionResult<ProductGetVm>> AddProduct(ProductAddVm productAddVm)
    {
        var productOutput = await _productService.AddAsync(productAddVm);

        return Ok(productOutput);
    }

    [HttpPut]
    public async Task<ActionResult<ProductGetVm>> UpdateProduct(ProductUpdateVm productUpdateVm)
    {
        var productOutput = await _productService.UpdateAsync(productUpdateVm);

        return Ok(productOutput);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ProductEntity>> RemoveProduct(long id)
    {
        await _productService.RemoveAsync(id);

        return Ok();
    }

    [HttpDelete]
    public async Task<ActionResult<ProductEntity>> ClearProducts()
    {
        await _productService.ClearAsync();

        return Ok();
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<ActionResult<ProductGetVm>> GetProduct(long id)
    {
        var productOutput = await _productService.GetByIdAsync(id);

        return Ok(productOutput);
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<List<ProductGetVm>>> GetAllProducts()
    {
        var productsOutput = await _productService.GetAllAsync();

        return Ok(productsOutput);
    }
}
