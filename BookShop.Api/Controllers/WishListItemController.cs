using BookShop.Services.Models.WishListItemModels;
using BookShop.Data.Entities;
using BookShop.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Api.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class WishListItemController : ControllerBase
{
    private readonly IWishListItemService _service;

    public WishListItemController(IWishListItemService service)
    {
        _service = service;
    }

    [HttpDelete]
    public async Task<ActionResult<WishListItemEntity>> RemoveItem(long wishListItemId)
    {
        await _service.RemoveAsync(wishListItemId);

        return Ok();
    }

    [HttpPost]
    public async Task<ActionResult<WishListItemGetVm>> AddItem(WishListItemAddVm wishListItemAddVm)
    {
        var wishListItemOutput = await _service.AddAsync(wishListItemAddVm);

        return Ok(wishListItemOutput);
    }
}
