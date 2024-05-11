using BookShop.Data;
using BookShop.Services.Abstractions;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using BookShop.Services.Models.WishListItemModels;

namespace BookShop.Services.Implementations;

internal class WishListService : IWishListService
{
    private readonly BookShopDbContext _dbContext;
    private readonly ILogger<WishListService> _logger;
    private readonly ICustomAuthenticationService _customAuthenticationService;
    private readonly IMapper _mapper;

    public WishListService(BookShopDbContext dbContext, ILogger<WishListService> logger,
        ICustomAuthenticationService customAuthenticationService, IMapper mapper)
    {
        _dbContext = dbContext;
        _logger = logger;
        _customAuthenticationService = customAuthenticationService;
        _mapper = mapper;
    }

    public async Task ClearAllItemsAsync(long wishListId)
    {
        var wishList = await _dbContext.WishLists.FirstOrDefaultAsync(w => w.Id == wishListId);

        if (wishList == null)
        {
            throw new Exception("WishList not found");
        }

        var client = await _dbContext.Clients.FirstOrDefaultAsync(c => c.Id == wishList.ClientId);

        if (client == null)
        {
            throw new Exception("Client not found");
        }

        var checkingClientEmail = _customAuthenticationService.GetClientEmailFromToken();

        if (client.Email != checkingClientEmail)
        {
            throw new Exception("Unauthorized: You can't clear Items from WishList of other Client");
        }

        if (wishList.WishListItems == null)
        {
            throw new Exception("WishList is already empty");
        }

        _dbContext.WishListItems.RemoveRange(wishList.WishListItems);
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation($"All WishListItems cleared from WishList with Id {wishListId} successfully");
    }

    public async Task<List<WishListItemGetVm>> GetAllItemsAsync(long wishListId)
    {
        var wishList = await _dbContext.WishLists.FirstOrDefaultAsync(w => w.Id == wishListId);

        if (wishList == null)
        {
            throw new Exception("WishList not found");
        }

        var client = await _dbContext.Clients.FirstOrDefaultAsync(c => c.Id == wishList.ClientId);

        if (client == null)
        {
            throw new Exception("Client not found");
        }

        var checkingClientEmail = _customAuthenticationService.GetClientEmailFromToken();

        if (client.Email != checkingClientEmail)
        {
            throw new Exception("Unauthorized: You can get Items only of your own WishList");
        }

        var wishListItemsGetVmList = new List<WishListItemGetVm>();
        var listItemsFromDb = await _dbContext.WishListItems.Where(ci => ci.WishListId == wishListId).ToListAsync();

        foreach( var WishListItem in listItemsFromDb)
        {
            var wishListItemGetVm = _mapper.Map<WishListItemGetVm>(WishListItem);
            wishListItemsGetVmList.Add(wishListItemGetVm);
        }

        return wishListItemsGetVmList;
    }
}
