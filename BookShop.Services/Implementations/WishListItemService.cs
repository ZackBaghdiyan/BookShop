using BookShop.Data.Entities;
using BookShop.Data;
using BookShop.Services.Abstractions;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using BookShop.Services.Models.WishListItemModels;

namespace BookShop.Services.Implementations;

internal class WishListItemService : IWishListItemService
{
    private readonly BookShopDbContext _dbContext;
    private readonly ILogger<WishListItemService> _logger;
    private readonly ICustomAuthenticationService _customAuthenticationService;
    private readonly IMapper _mapper;

    public WishListItemService(BookShopDbContext dbContext, ILogger<WishListItemService> logger,
        ICustomAuthenticationService customAuthenticationService, IMapper mapper)
    {
        _dbContext = dbContext;
        _logger = logger;
        _customAuthenticationService = customAuthenticationService;
        _mapper = mapper;
    }

    public async Task<WishListItemGetVm> AddAsync(WishListItemAddVm wishListItemAddVm)
    {
        if (wishListItemAddVm == null)
        {
            throw new Exception("There is nothing to add");
        }

        var wishList = await _dbContext.WishLists.FirstOrDefaultAsync(w => w.Id == wishListItemAddVm.WishListId);

        if (wishList == null)
        {
            throw new Exception("WishList not found");
        }

        var client = await _dbContext.Clients.FirstOrDefaultAsync(c => c.Id == wishList.ClientId);

        if (client == null)
        {
            throw new Exception("Client not Found");
        }

        var checkingClientEmail = _customAuthenticationService.GetClientEmailFromToken();

        if (client.Email != checkingClientEmail)
        {
            throw new Exception("Unauthorized: You can add Items only in your own WishList");
        }

        if (wishList.WishListItems == null)
        {
            wishList.WishListItems = new List<WishListItemEntity>();
        }

        var wishListItem = _mapper.Map<WishListItemEntity>(wishListItemAddVm);

        _dbContext.WishListItems.Add(wishListItem);
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation($"WishListItem with Id {wishListItem.Id} added to WishList with Id {wishList.Id} successfully");

        var wishListItemGetVm = _mapper.Map<WishListItemGetVm>(wishListItem);

        return wishListItemGetVm;
    }

    public async Task RemoveAsync(long wishListItemId)
    {
        var wishListItem = await _dbContext.WishListItems.FirstOrDefaultAsync(wi => wi.Id == wishListItemId);

        if (wishListItem == null)
        {
            throw new Exception("WishListItem not found");
        }

        var wishList = await _dbContext.WishLists.FirstOrDefaultAsync(w => w.Id == wishListItem.WishListId);

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
            throw new Exception("Unauthorized: You can remove Items only from your own WishList");
        }

        if (wishList.WishListItems == null)
        {
            throw new Exception("WishList is already empty");
        }

        _dbContext.WishListItems.Remove(wishListItem);
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation($"WishListItem with Id {wishListItem.Id} removed from WishList with Id {wishList.Id} successfully");
    }
}
