using AutoMapper;
using BookShop.Api.Models.WishListItemModels;
using BookShop.Api.Models.WishListModels;
using BookShop.Data.Entities;
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
    private readonly IMapper _mapper;

    public WishListController(IWishListService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<ActionResult<WishListEntity>> Create(WishListPostModel wishListToCreate)
    {
        var wishList = _mapper.Map<WishListEntity>(wishListToCreate);

        await _service.CreateAsync(wishList.ClientId);

        return Ok();
    }

    [HttpGet]
    public async Task<ActionResult<List<WishListItemGetModel>>> GetAllItems(long wishListId)
    {
        var wishListItems = await _service.GetAllItemsAsync(wishListId);
        var itemsOutput = new List<WishListItemGetModel>();

        foreach (var item in wishListItems)
        {
            var itemOutput = _mapper.Map<WishListItemGetModel>(item);
            itemsOutput.Add(itemOutput);
        }

        return Ok(itemsOutput);
    }
}
