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
    public async Task<ActionResult<List<CartItemGetVm>>> GetAllItems(long cartId)
    {
        var cartItems = await _service.GetAllItemsAsync(cartId);
        
        return Ok(cartItems);
    }
}
