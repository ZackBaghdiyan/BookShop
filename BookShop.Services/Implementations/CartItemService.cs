using AutoMapper;
using BookShop.Common.ClientService.Abstractions;
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
    private readonly IClientContextReader _clientContextReader;

    public CartItemService(BookShopDbContext dbContext, ILogger<CartItemService> logger,
        IMapper mapper, IClientContextReader clientContextReader)
    {
        _dbContext = dbContext;
        _logger = logger;
        _mapper = mapper;
        _clientContextReader = clientContextReader;
    }

    public async Task<CartItemModel> AddAsync(CartItemAddModel cartItemAddModel)
    {
        if (cartItemAddModel.Count <= 0)
        {
            throw new Exception("Product Count can't be less than 0");
        }

        var clientId = _clientContextReader.GetClientContextId();

        var cart = await _dbContext.Carts.Include(c => c.CartItems).FirstOrDefaultAsync(c => c.ClientId == clientId);
        var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == cartItemAddModel.ProductId);

        if (product.Count < cartItemAddModel.Count)
        {
            throw new Exception("Not enough product");
        }

        var cartItemCheck = cart.CartItems.FirstOrDefault(ci => ci.ProductId == cartItemAddModel.ProductId && ci.CartId == cart.Id);

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
        _logger.LogInformation($"CartItem with Id {cartItem.Id} added successfully for Client with Id {clientId}");

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
        _logger.LogInformation($"CartItem with Id {cartItemId} removed successfully for Client with Id {clientId}");
    }

    public async Task<CartItemModel> UpdateAsync(CartItemUpdateModel cartItemUpdateModel)
    {
        if(cartItemUpdateModel.Count <= 0)
        {
            throw new Exception("Product Count can't be less than 0");
        }

        var clientId = _clientContextReader.GetClientContextId();

        var cart = await _dbContext.Carts.Include(c => c.CartItems).FirstOrDefaultAsync(c => c.ClientId == clientId);
        var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == cartItemUpdateModel.ProductId);

        if (product.Count < cartItemUpdateModel.Count)
        {
            throw new Exception("Not enough product");
        }

        var cartItemToUpdate = cart.CartItems.FirstOrDefault(ci => ci.Id == cartItemUpdateModel.Id);

        cartItemToUpdate.Count = cartItemUpdateModel.Count;
        cartItemToUpdate.Price = cartItemToUpdate.Count * product.Price;

        await _dbContext.SaveChangesAsync();
        _logger.LogInformation($"CartItem with Id {cartItemToUpdate.Id} updated successfully for Client with Id {clientId}");

        var cartItemModel = _mapper.Map<CartItemModel>(cartItemToUpdate);

        return cartItemModel;
    }
}
