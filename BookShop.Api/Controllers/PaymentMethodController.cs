using BookShop.Services.Models.PaymentMethodModels;
using BookShop.Data.Entities;
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
    public async Task<ActionResult<PaymentMethodGetVm>> AddPaymentMethod(PaymentMethodAddVm paymentMethodAddVm)
    {
        var paymentMethodOutput = await _service.AddAsync(paymentMethodAddVm);

        return Ok(paymentMethodOutput);
    }

    [HttpDelete]
    public async Task<ActionResult<PaymentMethodEntity>> RemovePaymentMethod(long paymentMethodId)
    {
        await _service.RemoveAsync(paymentMethodId);

        return Ok();
    }

    [HttpGet]
    public async Task<ActionResult<List<PaymentMethodGetVm>>> GetAllPaymentMethods(long clientId)
    {
        var paymentMethodsOutput = await _service.GetAllAsync(clientId);

        return Ok(paymentMethodsOutput);
    }
}
