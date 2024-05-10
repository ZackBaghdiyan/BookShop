using BookShop.Data;
using BookShop.Data.Entities;
using BookShop.Services.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BookShop.Services.Implementations;

internal class CartService : ICartService
{
    private readonly BookShopDbContext _dbContext;
    private readonly ILogger<CartService> _logger;
    private readonly ICustomAuthenticationService _customAuthenticationService;

    public CartService(BookShopDbContext dbContext, ILogger<CartService> logger, ICustomAuthenticationService customAuthenticationService)
    {
        _dbContext = dbContext;
        _logger = logger;
        _customAuthenticationService = customAuthenticationService;
    }

    public async Task AddItemAsync(CartItemEntity cartItem)
    {
        try
        {
            if (cartItem == null)
            {
                throw new Exception("There is nothing to add");
            }

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
                throw new Exception("Unauthorized: You can only add Items in your own Cart");
            }

            if (cart.CartItems == null)
            {
                cart.CartItems = new List<CartItemEntity>();
            }

            cart.CartItems.Add(cartItem);
            await _dbContext.SaveChangesAsync();
            _logger.LogInformation($"CartItem with Id {cartItem.Id} added to Cart with Id {cart.Id} successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error: {ex.Message}");
            throw;
        }
    }

    public async Task CreateAsync(long clientId)
    {
        try
        {
            var cart = await _dbContext.Carts.FirstOrDefaultAsync(c => c.Id == clientId);

            if (cart != null)
            {
                throw new Exception($"Cart with ClientId {clientId} already exists");
            }

            var client = await _dbContext.Clients.FirstOrDefaultAsync(c => c.Id == clientId);

            if (client == null)
            {
                throw new Exception("Client not found");
            }

            var checkingClientEmail = _customAuthenticationService.GetClientEmailFromToken();

            if (client.Email != checkingClientEmail)
            {
                throw new Exception("Unauthorized: You can't create Cart for other Client");
            }

            var cartToAdd = new CartEntity { ClientId = clientId };

            _dbContext.Carts.Add(cartToAdd);
            await _dbContext.SaveChangesAsync();
            _logger.LogInformation($"Cart with Id {cartToAdd.Id} added successfully for Client with Id {clientId}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error: {ex.Message}");
            throw;
        }
    }

    public async Task RemoveItemAsync(CartItemEntity cartItem)
    {
        try
        {
            if (cartItem == null)
            {
                throw new Exception("There is nothing to add");
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
                throw new Exception("Unauthorized: You can only remove Items only from your own Cart");
            }

            if (cart.CartItems == null)
            {
                throw new Exception("Cart is already empty");
            }

            cart.CartItems.Remove(cartItem);
            await _dbContext.SaveChangesAsync();
            _logger.LogInformation($"CartItem with Id {cartItem.Id} removed from Cart with Id {cart.Id} successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error: {ex.Message}");
            throw;
        }
    }

    public async Task<List<CartItemEntity>> GetAllItemsAsync(long cartId)
    {
        try
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
                throw new Exception("Unauthorized: You can only get Items of your own cart");
            }

            return cart.CartItems;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error: {ex.Message}");
            throw;
        }
    }
}

