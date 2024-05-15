using BookShop.Data;
using BookShop.Services.Abstractions;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using BookShop.Services.Models.WishListItemModels;
using BookShop.Common.ClientService.Abstractions;

namespace BookShop.Services.Implementations;

internal class WishListService : IWishListService
{
    private readonly BookShopDbContext _dbContext;
    private readonly ILogger<WishListService> _logger;
    private readonly IMapper _mapper;
    private readonly IClientContextReader _clientContextReader;

    public WishListService(BookShopDbContext dbContext, ILogger<WishListService> logger,
        IMapper mapper, IClientContextReader clientContextReader)
    {
        _dbContext = dbContext;
        _logger = logger;
        _mapper = mapper;
        _clientContextReader = clientContextReader;
    }

    public async Task<List<WishListItemModel>> GetAllItemsAsync()
    {
        var clientId = _clientContextReader.GetClientContextId();

        var wishList = await _dbContext.WishLists.Include(w => w.WishListItems).FirstOrDefaultAsync(w => w.ClientId == clientId);

        var wishListItemModels = new List<WishListItemModel>();

        foreach (var wishListItem in wishList.WishListItems)
        {
            var wishListItemModel = _mapper.Map<WishListItemModel>(wishListItem);

            wishListItemModels.Add(wishListItemModel);
        }

        return wishListItemModels;
    }

    public async Task ClearAllItemsAsync()
    {
        var clientId = _clientContextReader.GetClientContextId();

        var wishList = await _dbContext.WishLists.Include(w => w.ClientEntity).FirstOrDefaultAsync(w => w.ClientId == clientId);

        _dbContext.WishListItems.RemoveRange(wishList.WishListItems);
        wishList.WishListItems.Clear();
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation("WishListItems cleared successfully");
    }
}
