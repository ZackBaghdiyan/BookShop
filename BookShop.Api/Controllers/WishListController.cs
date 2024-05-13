using BookShop.Services.Models.WishListItemModels;
using BookShop.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Api.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class WishListController : ControllerBase
{
    private readonly IWishListService _service;

    public WishListController(IWishListService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<WishListItemModel>>> GetAllItems()
    {
        var wishListItemsOutput = await _service.GetAllItemsAsync();

        return Ok(wishListItemsOutput);
    }

    [HttpDelete]
    public async Task<IActionResult> ClearAllItems()
    {
        await _service.ClearAllItemsAsync();

        return Ok();
    }
}
