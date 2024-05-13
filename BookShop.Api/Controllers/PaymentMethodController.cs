using BookShop.Services.Models.PaymentMethodModels;
using BookShop.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Api.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class PaymentMethodController : ControllerBase
{
    private readonly IPaymentMethodService _service;

    public PaymentMethodController(IPaymentMethodService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<ActionResult<PaymentMethodModel>> AddPaymentMethod(PaymentMethodAddModel paymentMethodAddModel)
    {
        var paymentMethodOutput = await _service.AddAsync(paymentMethodAddModel);

        return Ok(paymentMethodOutput);
    }

    [HttpDelete]
    public async Task<IActionResult> RemovePaymentMethod(long paymentMethodId)
    {
        await _service.RemoveAsync(paymentMethodId);

        return Ok();
    }

    [HttpGet]
    public async Task<ActionResult<List<PaymentMethodModel>>> GetAllPaymentMethods()
    {
        var paymentMethodsOutput = await _service.GetAllAsync();

        return Ok(paymentMethodsOutput);
    }
}
