using BookShop.Services.Abstractions;
using BookShop.Services.Models.OrderModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Api.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _service;

    public OrderController(IOrderService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<ActionResult<OrderModel>> AddItem(OrderAddModel order)
    {
        var orderOutput = await _service.AddAsync(order);

        return Ok(orderOutput);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveOrder(long id)
    {
        await _service.RemoveAsync(id);

        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> Clear()
    {
        await _service.ClearAsync();

        return Ok();
    }
}
