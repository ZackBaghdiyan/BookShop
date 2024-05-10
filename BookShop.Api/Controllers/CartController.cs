using AutoMapper;
using BookShop.Api.Models.CartItemModels;
using BookShop.Api.Models.CartModels;
using BookShop.Data.Entities;
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
    private readonly IMapper _mapper;

    public CartController(ICartService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<ActionResult<CartEntity>> Create(CartPostModel cartToCreate)
    {
        var cart = _mapper.Map<CartEntity>(cartToCreate);

        await _service.CreateAsync(cart.ClientId);

        return Ok();
    }

    [HttpGet]
    public async Task<ActionResult<List<CartItemGetModel>>> GetAllItems(long cartId)
    {
        var cartItems = await _service.GetAllItemsAsync(cartId);
        var itemsOutput = new List<CartItemGetModel>();

        foreach (var item in cartItems)
        {
            var itemOutput = _mapper.Map<CartItemGetModel>(item);
            itemsOutput.Add(itemOutput);
        }

        return Ok(itemsOutput);
    }
}
