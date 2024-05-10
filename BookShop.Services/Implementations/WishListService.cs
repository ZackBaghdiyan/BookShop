using BookShop.Data.Entities;
using BookShop.Data;
using BookShop.Services.Abstractions;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace BookShop.Services.Implementations;

internal class WishListService : IWishListService
{
    private readonly BookShopDbContext _dbContext;
    private readonly ILogger<WishListService> _logger;
    private readonly ICustomAuthenticationService _customAuthenticationService;

    public WishListService(BookShopDbContext dbContext, ILogger<WishListService> logger,
        ICustomAuthenticationService customAuthenticationService)
    {
        _dbContext = dbContext;
        _logger = logger;
        _customAuthenticationService = customAuthenticationService;
    }

    public async Task ClearAllItemsAsync(long wishListId)
    {
        try
        {
            var wishList = await _dbContext.WishLists.FirstOrDefaultAsync(c => c.Id == wishListId);

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
            var wishList = await _dbContext.WishLists.FirstOrDefaultAsync(c => c.Id == clientId);

            if (wishList != null)
            {
                throw new Exception($"WishList with ClientId {clientId} already exists");
            }

            var client = await _dbContext.Clients.FirstOrDefaultAsync(c => c.Id == clientId);

            if (client == null)
            {
                throw new Exception("Client not found");
            }

            var checkingClientEmail = _customAuthenticationService.GetClientEmailFromToken();

            if (client.Email != checkingClientEmail)
            {
                throw new Exception("Unauthorized: You can't create WishList for other Client");
            }

            var wishListToAdd = new WishListEntity { ClientId = clientId };

            _dbContext.WishLists.Add(wishListToAdd);
            await _dbContext.SaveChangesAsync();
            _logger.LogInformation($"WishList with Id {wishListToAdd.Id} added successfully for Client with Id {clientId}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error: {ex.Message}");
            throw;
        }
    }

    public async Task<List<WishListItemEntity>> GetAllItemsAsync(long wishListId)
    {
        try
        {
            var wishList = await _dbContext.WishLists.FirstOrDefaultAsync(c => c.Id == wishListId);

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

            var listToReturn = new List<WishListItemEntity>();
            var listItemsFromDb = await _dbContext.WishListItems.Where(ci => ci.WishListId == wishListId).ToListAsync();

            listToReturn.AddRange(listItemsFromDb);

            return listToReturn;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error: {ex.Message}");
            throw;
        }
    }
}
