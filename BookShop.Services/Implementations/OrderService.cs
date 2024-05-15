using AutoMapper;
using BookShop.Common.ClientService.Abstractions;
using BookShop.Data;
using BookShop.Data.Entities;
using BookShop.Services.Abstractions;
using BookShop.Services.Models.OrderModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BookShop.Services.Implementations;

internal class OrderService : IOrderService
{
    private readonly IClientContextReader _clientContextReader;
    private readonly ILogger<OrderService> _logger;
    private readonly IMapper _mapper;
    private readonly BookShopDbContext _dbContext;

    public OrderService(IClientContextReader clientContextReader, ILogger<OrderService> logger, IMapper mapper, BookShopDbContext dbContext)
    {
        _clientContextReader = clientContextReader;
        _logger = logger;
        _mapper = mapper;
        _dbContext = dbContext;
    }
    public async Task<OrderModel> AddAsync(OrderAddModel orderAddModel)
    {
        var clientId = _clientContextReader.GetClientContextId();

        var order = await _dbContext.Orders.FirstOrDefaultAsync(o => o.ClientId == clientId && o.ProductId == orderAddModel.ProductId);
        var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == orderAddModel.ProductId);

        var orderModel = new OrderModel();

        if (order != null)
        {
            order.Count += orderAddModel.Count;
            order.Amount = product.Price * order.Count;

            await _dbContext.SaveChangesAsync();

            _logger.LogInformation($"Order with Id {order.Id} added successfully for Client with Id {clientId}");

            orderModel = _mapper.Map<OrderModel>(order);

            return orderModel;
        }

        var orderToAdd = _mapper.Map<OrderEntity>(orderAddModel);

        orderToAdd.Amount = product.Price * orderAddModel.Count;
        orderToAdd.ClientId = clientId;

        _dbContext.Orders.Add(orderToAdd);
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation($"Order with Id {orderToAdd.Id} added successfully for Client with Id {clientId}");

        orderModel = _mapper.Map<OrderModel>(orderToAdd);

        return orderModel;
    }

    public async Task RemoveAsync(long orderId)
    {
        var clientId = _clientContextReader.GetClientContextId();

        var order = await _dbContext.Orders.FirstOrDefaultAsync(o => o.ClientId == clientId && o.Id == orderId);

        _dbContext.Orders.Remove(order);
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation($"Order with Id {orderId} removed successfully for Client with Id {clientId}");
    }

    public async Task ClearAsync()
    {
        var clientId = _clientContextReader.GetClientContextId();

        var orders = await _dbContext.Orders.Where(o => o.ClientId == clientId).ToListAsync();

        _dbContext.Orders.RemoveRange(orders);
        await _dbContext.SaveChangesAsync();

        _logger.LogInformation($"Orders cleared successfully for Client with Id {clientId}");
    }
}
