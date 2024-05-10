using AutoMapper;
using BookShop.Api.Models.CartItemModels;
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
    private readonly IMapper _mapper;

    public CartItemController(ICartItemService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpDelete]
    public async Task<ActionResult<CartItemEntity>> RemoveItem(CartItemDeleteModel cartItem)
    {
        var cartItemToRemove = _mapper.Map<CartItemEntity>(cartItem);
        await _service.RemoveAsync(cartItemToRemove);

        return Ok();
    }

    [HttpPost]
    public async Task<ActionResult<CartItemEntity>> AddItem(CartItemPostModel cartItem)
    {
        var cartItemToAdd = _mapper.Map<CartItemEntity>(cartItem);
        await _service.AddAsync(cartItemToAdd);

        return Ok();
    }

    [HttpPut]
    public async Task<ActionResult<CartItemEntity>> UpdateItem(CartItemPutModel cartItem)
    {
        var cartItemToUpdate = _mapper.Map<CartItemEntity>(cartItem);
        await _service.UpdateAsync(cartItemToUpdate);

        return Ok();
    }
}
