using BookShop.Services.Models.ProductModels;
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
    public async Task<ActionResult<ProductModel>> AddProduct(ProductAddModel productAddModel)
    {
        var productOutput = await _productService.AddAsync(productAddModel);

        return Ok(productOutput);
    }

    [HttpPut]
    public async Task<ActionResult<ProductModel>> UpdateProduct(ProductUpdateModel productUpdateModel)
    {
        var productOutput = await _productService.UpdateAsync(productUpdateModel);

        return Ok(productOutput);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveProduct(long id)
    {
        await _productService.RemoveAsync(id);

        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> ClearProducts()
    {
        await _productService.ClearAsync();

        return Ok();
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<List<ProductModel>>> GetAllProducts()
    {
        var productsOutput = await _productService.GetAllAsync();

        return Ok(productsOutput);
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<ActionResult<ProductModel>> GetProduct(long id)
    {
        var productOutput = await _productService.GetByIdAsync(id);

        return Ok(productOutput);
    }
}
