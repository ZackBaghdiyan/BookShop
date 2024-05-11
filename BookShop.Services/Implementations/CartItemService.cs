using AutoMapper;
using BookShop.Data;
using BookShop.Data.Entities;
using BookShop.Services.Abstractions;
using BookShop.Services.Models.CartItemModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BookShop.Services.Implementations;

internal class CartItemService : ICartItemService
{
    private readonly BookShopDbContext _dbContext;
    private readonly ILogger<CartItemService> _logger;
    private readonly ICustomAuthenticationService _customAuthenticationService;
    private readonly IMapper _mapper;

    public CartItemService(BookShopDbContext dbContext, ILogger<CartItemService> logger,
        ICustomAuthenticationService customAuthenticationService, IMapper mapper)
    {
        _dbContext = dbContext;
        _logger = logger;
        _customAuthenticationService = customAuthenticationService;
        _mapper = mapper;
    }

    public async Task<CartItemGetVm> AddAsync(CartItemAddVm cartItemAddVm)
    {
        if (cartItemAddVm == null)
        {
            throw new Exception("There is nothing to add");
        }

        var cartItem = _mapper.Map<CartItemEntity>(cartItemAddVm);

        var cart = await _dbContext.Carts.FirstOrDefaultAsync(c => c.Id == cartItem.CartId);

        if (cart == null)
        {
            throw new Exception("Cart not found");
        }

        var client = await _dbContext.Clients.FirstOrDefaultAsync(c => c.Id == cart.ClientId);

        if (client == null)
        {
            throw new Exception("Client not Found");
        }

        var checkingClientEmail = _customAuthenticationService.GetClientEmailFromToken();

        if (client.Email != checkingClientEmail)
        {
            throw new Exception("Unauthorized: You can add Items only in your own Cart");
        }

        if (cart.CartItems == null)
        {
            cart.CartItems = new List<CartItemEntity>();
        }

        cartItem.Price = cartItem.Price * cartItem.Count;
        _dbContext.CartItems.Add(cartItem);
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation($"CartItem with Id {cartItem.Id} added to Cart with Id {cart.Id} successfully");

        var cartItemGetVm = _mapper.Map<CartItemGetVm>(cartItem);

        return cartItemGetVm;
    }

    public async Task RemoveAsync(long cartItemId)
    {
        var cartItem = await _dbContext.CartItems.FirstOrDefaultAsync(ci => ci.Id == cartItemId);

        if(cartItem == null)
        {
            throw new Exception("CartItem not found");
        }

        var cart = await _dbContext.Carts.FirstOrDefaultAsync(c => c.Id == cartItem.CartId);

        if (cart == null)
        {
            throw new Exception("Cart not found");
        }

        var client = await _dbContext.Clients.FirstOrDefaultAsync(c => c.Id == cart.ClientId);

        if (client == null)
        {
            throw new Exception("Client not found");
        }

        var checkingClientEmail = _customAuthenticationService.GetClientEmailFromToken();

        if (client.Email != checkingClientEmail)
        {
            throw new Exception("Unauthorized: You can remove Items only from your own Cart");
        }

        if (cart.CartItems == null)
        {
            throw new Exception("Cart is already empty");
        }

        _dbContext.CartItems.Remove(cartItem);
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation($"CartItem with Id {cartItem.Id} removed from Cart with Id {cart.Id} successfully");
    }

    public async Task<CartItemGetVm> UpdateAsync(CartItemUpdateVm cartItemUpdateVm)
    {
        if (cartItemUpdateVm == null)
        {
            throw new Exception("There is nothing to add");
        }

        var cartItem = _mapper.Map<CartItemEntity>(cartItemUpdateVm);

        var cart = await _dbContext.Carts.FirstOrDefaultAsync(c => c.Id == cartItem.CartId);

        if (cart == null)
        {
            throw new Exception("Cart not found");
        }

        var client = await _dbContext.Clients.FirstOrDefaultAsync(c => c.Id == cart.ClientId);

        if (client == null)
        {
            throw new Exception("Client not found");
        }

        var checkingClientEmail = _customAuthenticationService.GetClientEmailFromToken();

        if (client.Email != checkingClientEmail)
        {
            throw new Exception("Unauthorized: You can update Items only from your own Cart");
        }

        var cartItemToUpdate = await _dbContext.CartItems.FirstOrDefaultAsync(ci => ci.Id == cartItem.Id);

        if (cartItemToUpdate == null)
        {
            throw new Exception("CartItem not found");
        }

        cartItemToUpdate.Count = cartItem.Count;
        cartItemToUpdate.Price = cartItem.Count * cartItem.Price;

        await _dbContext.SaveChangesAsync();
        _logger.LogInformation($"CartItem with Id {cartItemToUpdate.Id} updated in Cart with Id {cart.Id} successfully");

        var cartItemGetVm = _mapper.Map<CartItemGetVm>(cartItemToUpdate);

        return cartItemGetVm;
    }
}
