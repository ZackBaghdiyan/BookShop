using AutoMapper;
using BookShop.Api.Models.WishListItemModels;
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
    private readonly IMapper _mapper;

    public WishListItemController(IWishListItemService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpDelete]
    public async Task<ActionResult<WishListItemEntity>> RemoveItem(WishListItemDeleteModel wishListItem)
    {
        var wishListItemToRemove = _mapper.Map<WishListItemEntity>(wishListItem);
        await _service.RemoveAsync(wishListItemToRemove);

        return Ok();
    }

    [HttpPost]
    public async Task<ActionResult<WishListItemEntity>> AddItem(WishListItemPostModel wishListItem)
    {
        var wishListItemToAdd = _mapper.Map<WishListItemEntity>(wishListItem);
        await _service.AddAsync(wishListItemToAdd);

        return Ok();
    }
}
