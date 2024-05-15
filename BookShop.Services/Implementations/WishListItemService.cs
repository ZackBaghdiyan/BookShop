using BookShop.Data.Entities;
using BookShop.Data;
using BookShop.Services.Abstractions;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using BookShop.Services.Models.WishListItemModels;
using BookShop.Common.ClientService.Abstractions;

namespace BookShop.Services.Implementations;

internal class WishListItemService : IWishListItemService
{
    private readonly BookShopDbContext _dbContext;
    private readonly ILogger<WishListItemService> _logger;
    private readonly IMapper _mapper;
    private readonly IClientContextReader _clientContextReader;

    public WishListItemService(BookShopDbContext dbContext, ILogger<WishListItemService> logger, 
        IMapper mapper, IClientContextReader clientContextReader)
    {
        _dbContext = dbContext;
        _logger = logger;
        _mapper = mapper;
        _clientContextReader = clientContextReader;
    }

    public async Task<WishListItemModel> AddAsync(WishListItemAddModel wishListItemAddModel)
    {
        var clientId = _clientContextReader.GetClientContextId();

        var wishList = await _dbContext.WishLists.Include(w => w.WishListItems).FirstOrDefaultAsync(w => w.ClientId == clientId);

        var wishListItem = _mapper.Map<WishListItemEntity>(wishListItemAddModel);

        wishListItem.WishListId = wishList.Id;

        _dbContext.WishListItems.Add(wishListItem);
        wishList.WishListItems.Add(wishListItem);
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation($"WishListItem with Id {wishListItem.Id} added successfully");

        var wishListItemModel = _mapper.Map<WishListItemModel>(wishListItem);

        return wishListItemModel;
    }

    public async Task RemoveAsync(long wishListItemId)
    {
        var clientId = _clientContextReader.GetClientContextId();

        var wishList = await _dbContext.WishLists.Include(w => w.WishListItems).FirstOrDefaultAsync(w => w.ClientId == clientId);

        var wishListItemToRemove = wishList.WishListItems.FirstOrDefault(wi => wi.Id == wishListItemId);

        _dbContext.WishListItems.Remove(wishListItemToRemove);
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation($"WishListItem with Id {wishListItemId} removed successfully");
    }
}
