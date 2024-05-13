using BookShop.Services.Models.CartItemModels;
using BookShop.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Api.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class CartController : ControllerBase
{
    private readonly ICartService _service;

    public CartController(ICartService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<CartItemModel>>> GetAllItems()
    {
        var cartItems = await _service.GetAllItemsAsync();
        
        return Ok(cartItems);
    }

    [HttpDelete]
    public async Task<IActionResult> ClearAllItems()
    {
        await _service.ClearAllItemsAsync();

        return Ok();
    }
}
