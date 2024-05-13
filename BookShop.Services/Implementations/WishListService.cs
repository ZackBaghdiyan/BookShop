using BookShop.Data;
using BookShop.Services.Abstractions;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using BookShop.Services.Models.WishListItemModels;
using BookShop.Common.ClientService;

namespace BookShop.Services.Implementations;

internal class WishListService : IWishListService
{
    private readonly BookShopDbContext _dbContext;
    private readonly ILogger<WishListService> _logger;
    private readonly IMapper _mapper;
    private readonly ClientContextReader _clientContextReader;

    public WishListService(BookShopDbContext dbContext, ILogger<WishListService> logger,
        IMapper mapper, ClientContextReader clientContextReader)
    {
        _dbContext = dbContext;
        _logger = logger;
        _mapper = mapper;
        _clientContextReader = clientContextReader;
    }

    public async Task ClearAllItemsAsync()
    {
        var clientId = _clientContextReader.GetClientContextId();

        var wishList = await _dbContext.WishLists.Include(w => w.ClientEntity).FirstOrDefaultAsync(w => w.ClientId == clientId);

        if (wishList == null)
        {
            throw new Exception("WishList not found");
        }

        var wishListItemsToClear = await _dbContext.WishListItems.Where(wi => wi.WishListId == wishList.Id).ToListAsync();

        _dbContext.WishListItems.RemoveRange(wishListItemsToClear);
        wishList.WishListItems.Clear();
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation("WishListItems cleared successfully");
    }

    public async Task<List<WishListItemModel>> GetAllItemsAsync()
    {
        var clientId = _clientContextReader.GetClientContextId();

        var wishList = await _dbContext.WishLists.Include(w => w.WishListItems).FirstOrDefaultAsync(w => w.ClientId == clientId);

        if (wishList == null)
        {
            throw new Exception("WishList not found");
        }

        var wishListItemsToReturn = await _dbContext.WishListItems.Where(wi => wi.WishListId == wishList.Id).ToListAsync();
        var wishListItemModels = new List<WishListItemModel>();

        foreach (var wishListItem in wishListItemsToReturn)
        {
            var wishListItemModel = _mapper.Map<WishListItemModel>(wishListItem);

            wishListItemModels.Add(wishListItemModel);
        }

        return wishListItemModels;
    }
}
