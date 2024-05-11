using AutoMapper;
using BookShop.Data;
using BookShop.Services.Abstractions;
using BookShop.Services.Models.CartItemModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BookShop.Services.Implementations;

internal class CartService : ICartService
{
    private readonly BookShopDbContext _dbContext;
    private readonly ILogger<CartService> _logger;
    private readonly ICustomAuthenticationService _customAuthenticationService;
    private readonly IMapper _mapper;

    public CartService(BookShopDbContext dbContext, ILogger<CartService> logger,
        ICustomAuthenticationService customAuthenticationService, IMapper mapper)
    {
        _dbContext = dbContext;
        _logger = logger;
        _customAuthenticationService = customAuthenticationService;
        _mapper = mapper;
    }

    public async Task ClearAllItemsAsync(long cartId)
    {
        var cart = await _dbContext.Carts.FirstOrDefaultAsync(c => c.Id == cartId);

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
            throw new Exception("Unauthorized: You can't clear Items from Cart of other Client");
        }

        if (cart.CartItems == null)
        {
            throw new Exception("Cart is already empty");
        }

        _dbContext.CartItems.RemoveRange(cart.CartItems);
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation($"All CartItems cleared from Cart with Id {cartId} successfully");
    }

    public async Task<List<CartItemGetVm>> GetAllItemsAsync(long cartId)
    {
        var cart = await _dbContext.Carts.FirstOrDefaultAsync(c => c.Id == cartId);

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
            throw new Exception("Unauthorized: You can get Items only of your own cart");
        }

        var cartItemGetVmList = new List<CartItemGetVm>();
        var cartItemsDb = await _dbContext.CartItems.Where(ci => ci.CartId == cartId).ToListAsync();

        foreach (var cartItem in cartItemsDb)
        {
            var listItemGetVm = _mapper.Map<CartItemGetVm>(cartItem);
            cartItemGetVmList.Add(listItemGetVm);
        }

        return cartItemGetVmList;
    }
}

