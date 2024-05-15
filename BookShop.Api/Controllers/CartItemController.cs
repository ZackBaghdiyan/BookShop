using BookShop.Services.Models.CartItemModels;
using BookShop.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Api.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class CartItemController : ControllerBase
{
    private readonly ICartItemService _service;

    public CartItemController(ICartItemService service)
    {
        _service = service;
    }

    [HttpDelete]
    public async Task<IActionResult> RemoveItem(long cartItemId)
    {
        await _service.RemoveAsync(cartItemId);

        return Ok();
    }

    [HttpPost]
    public async Task<ActionResult<CartItemModel>> AddItem(CartItemAddModel cartItem)
    {
        var cartItemOutput = await _service.AddAsync(cartItem);

        return Ok(cartItemOutput);
    }

    [HttpPut]
    public async Task<ActionResult<CartItemModel>> UpdateItem(CartItemUpdateModel cartItem)
    {
        var cartItemOutput =await _service.UpdateAsync(cartItem);

        return Ok(cartItemOutput);
    }
}
