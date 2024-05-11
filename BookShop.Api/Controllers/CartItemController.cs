using BookShop.Services.Models.CartItemModels;
using BookShop.Data.Entities;
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
    public async Task<ActionResult<CartItemEntity>> RemoveItem(long carItemId)
    {
        await _service.RemoveAsync(carItemId);

        return Ok();
    }

    [HttpPost]
    public async Task<ActionResult<CartItemGetVm>> AddItem(CartItemAddVm cartItem)
    {
        var cartItemOutput = await _service.AddAsync(cartItem);

        return Ok(cartItemOutput);
    }

    [HttpPut]
    public async Task<ActionResult<CartItemGetVm>> UpdateItem(CartItemUpdateVm cartItem)
    {
        var cartItemOutput =await _service.UpdateAsync(cartItem);

        return Ok(cartItemOutput);
    }
}
