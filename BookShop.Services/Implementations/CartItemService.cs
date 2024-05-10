using BookShop.Data;
using BookShop.Data.Entities;
using BookShop.Services.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BookShop.Services.Implementations;

internal class CartItemService : ICartItemService
{
    private readonly BookShopDbContext _dbContext;
    private readonly ILogger<CartItemService> _logger;
    private readonly ICustomAuthenticationService _customAuthenticationService;

    public CartItemService(BookShopDbContext dbContext, ILogger<CartItemService> logger,
        ICustomAuthenticationService customAuthenticationService)
    {
        _dbContext = dbContext;
        _logger = logger;
        _customAuthenticationService = customAuthenticationService;
    }

    public async Task AddAsync(CartItemEntity cartItem)
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

            cartItem.Price = cartItem.Price * cartItem.Count;
            _dbContext.CartItems.Add(cartItem);
            await _dbContext.SaveChangesAsync();
            _logger.LogInformation($"CartItem with Id {cartItem.Id} added to Cart with Id {cart.Id} successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error: {ex.Message}");
            throw;
        }
    }

    public async Task RemoveAsync(CartItemEntity cartItem)
    {
        try
        {
            if (cartItem == null)
            {
                throw new Exception("There is nothing to remove");
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

            _dbContext.CartItems.Remove(cartItem);
            await _dbContext.SaveChangesAsync();
            _logger.LogInformation($"CartItem with Id {cartItem.Id} removed from Cart with Id {cart.Id} successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error: {ex.Message}");
            throw;
        }
    }

    public async Task UpdateAsync(CartItemEntity cartItem)
    {
        try
        {
            if (cartItem == null)
            {
                throw new Exception("There is nothing to remove");
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

            var cartItemToUpdate = await _dbContext.CartItems.FirstOrDefaultAsync(c => c.Id == cartItem.Id);

            if (cartItemToUpdate == null)
            {
                throw new Exception("CartItem not found");
            }

            cartItemToUpdate.Count = cartItem.Count;
            cartItemToUpdate.Price = cartItem.Count * cartItem.Price;

            await _dbContext.SaveChangesAsync();
            _logger.LogInformation($"CartItem with Id {cartItem.Id} removed from Cart with Id {cart.Id} successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error: {ex.Message}");
            throw;
        }
    }
}
