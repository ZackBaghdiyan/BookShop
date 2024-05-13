using AutoMapper;
using BookShop.Common.ClientService;
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
    private readonly IMapper _mapper;
    private readonly ClientContextReader _clientContextReader;

    public CartService(BookShopDbContext dbContext, ILogger<CartService> logger, IMapper mapper, ClientContextReader clientContextReader)
    {
        _dbContext = dbContext;
        _logger = logger;
        _mapper = mapper;
        _clientContextReader = clientContextReader;
    }

    public async Task ClearAllItemsAsync()
    {
        var clientId = _clientContextReader.GetClientContextId();

        var cart = await _dbContext.Carts.Include(c => c.Client).FirstOrDefaultAsync(c => c.ClientId == clientId);

        if (cart == null)
        {
            throw new Exception("Cart not found");
        }

        var cartItemsToClear = await _dbContext.CartItems.Where(ci => ci.CartId == cart.Id).ToListAsync();

        _dbContext.CartItems.RemoveRange(cartItemsToClear);
        cart.CartItems.Clear();
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation("CartItems cleared successfully");
    }

    public async Task<List<CartItemModel>> GetAllItemsAsync()
    {
        var clientId = _clientContextReader.GetClientContextId();

        var cart = await _dbContext.Carts.Include(c => c.CartItems).FirstOrDefaultAsync(c => c.ClientId == clientId);

        if (cart == null)
        {
            throw new Exception("Client not found");
        }

        var cartItemsToReturn = await _dbContext.CartItems.Where(ci => ci.CartId == cart.Id).ToListAsync();
        var cartItemModels = new List<CartItemModel>();

        foreach (var cartItem in cartItemsToReturn)
        {
            var cartItemModel = _mapper.Map<CartItemModel>(cartItem);

            cartItemModels.Add(cartItemModel);
        }

        return cartItemModels;
    }
}

