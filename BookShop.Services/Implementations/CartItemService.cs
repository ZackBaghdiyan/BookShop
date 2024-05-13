using AutoMapper;
using BookShop.Common.ClientService;
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
    private readonly IMapper _mapper;
    private readonly ClientContextReader _clientContextReader;

    public CartItemService(BookShopDbContext dbContext, ILogger<CartItemService> logger,
        IMapper mapper, ClientContextReader clientContextReader)
    {
        _dbContext = dbContext;
        _logger = logger;
        _mapper = mapper;
        _clientContextReader = clientContextReader;
    }

    public async Task<CartItemModel> AddAsync(CartItemAddModel cartItemAddModel)
    {
        var clientId = _clientContextReader.GetClientContextId();

        var cart = await _dbContext.Carts.Include(c => c.CartItems).FirstOrDefaultAsync(c => c.ClientId == clientId);
        var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == cartItemAddModel.ProductId);

        if (cart == null)
        {
            throw new Exception("Cart not found");
        }
        else if (product == null)
        {
            throw new Exception("Product not found");
        }
        else if (product.Count < cartItemAddModel.Count)
        {
            cartItemAddModel.Count = product.Count;
        }

        var cartItemCheck = _dbContext.CartItems.FirstOrDefault(ci => ci.ProductId == cartItemAddModel.ProductId);

        var cartItemModel = new CartItemModel();

        if (cartItemCheck != null)
        {
            cartItemCheck.Count += cartItemAddModel.Count;
            cartItemCheck.Price = cartItemCheck.Count * product.Price;
            await _dbContext.SaveChangesAsync();

            cartItemModel = _mapper.Map<CartItemModel>(cartItemCheck);

            return cartItemModel;
        }

        var cartItem = _mapper.Map<CartItemEntity>(cartItemAddModel);

        cartItem.Price = cartItem.Count * product.Price;
        cartItem.CartId = cart.Id;

        _dbContext.CartItems.Add(cartItem);
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation($"CartItem with Id {cartItem.Id} added successfully");

        cartItemModel = _mapper.Map<CartItemModel>(cartItem);

        return cartItemModel;
    }

    public async Task RemoveAsync(long cartItemId)
    {
        var clientId = _clientContextReader.GetClientContextId();

        var cart = await _dbContext.Carts.Include(c => c.CartItems).FirstOrDefaultAsync(c => c.ClientId == clientId);

        if (cart == null)
        {
            throw new Exception("Cart not found");
        }

        var cartItemToRemove = cart.CartItems.FirstOrDefault(ci => ci.Id == cartItemId);

        _dbContext.CartItems.Remove(cartItemToRemove);
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation($"CartItem with Id {cartItemId} removed successfully");
    }

    public async Task<CartItemModel> UpdateAsync(CartItemUpdateModel cartItemUpdateModel)
    {
        var clientId = _clientContextReader.GetClientContextId();

        var cart = await _dbContext.Carts.Include(c => c.CartItems).FirstOrDefaultAsync(c => c.ClientId == clientId);
        var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == cartItemUpdateModel.ProductId);

        if (cart == null)
        {
            throw new Exception("Cart not found");
        }
        else if (product == null)
        {
            throw new Exception("Product not found");
        }
        else if (product.Count < cartItemUpdateModel.Count)
        {
            cartItemUpdateModel.Count = product.Count;
        }

        var cartItemToUpdate = await _dbContext.CartItems.FirstOrDefaultAsync(ci => ci.Id == cartItemUpdateModel.Id
        && ci.CartEntity.ClientId == clientId);

        if (cartItemToUpdate == null)
        {
            throw new Exception("CartItem not found");
        }

        cartItemToUpdate = _mapper.Map<CartItemEntity>(cartItemUpdateModel);

        cartItemToUpdate.Count = cartItemUpdateModel.Count;
        cartItemToUpdate.Price = cartItemUpdateModel.Count * product.Price;

        _logger.LogInformation($"CartItem with Id {cartItemToUpdate.Id} updated successfully");

        var cartItemToDelete = cart.CartItems.FirstOrDefault(ci => ci.ProductId == cartItemToUpdate.ProductId);
        cart.CartItems.Remove(cartItemToDelete);
        cart.CartItems.Add(cartItemToUpdate);
        await _dbContext.SaveChangesAsync();

        var cartItemModel = _mapper.Map<CartItemModel>(cartItemToUpdate);

        return cartItemModel;
    }
}
