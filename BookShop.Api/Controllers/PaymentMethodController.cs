using AutoMapper;
using BookShop.Api.Models.PaymentMethodModels;
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
    private readonly IMapper _mapper;

    public PaymentMethodController(IPaymentMethodService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<ActionResult<PaymentMethodEntity>> AddPaymentMethod(PaymentMethodPostModel paymentMethod)
    {
        var paymentMethodToAdd = _mapper.Map<PaymentMethodEntity>(paymentMethod);
        await _service.AddAsync(paymentMethodToAdd);

        return Ok();
    }
    
    [HttpDelete]
    public async Task<ActionResult<PaymentMethodEntity>> RemovePaymentMethod(PaymentMethodDeleteModel paymentMethodInput)
    {
        var paymentMethod = _mapper.Map<PaymentMethodEntity>(paymentMethodInput);
        await _service.RemoveAsync(paymentMethod);

        return Ok();
    }
    
    [HttpGet]
    public async Task<ActionResult<List<PaymentMethodGetModel>>> GetAllPaymentMethods(long clientId)
    {
        var paymentMethods = await _service.GetAllAsync(clientId);
        var paymentMethodsOutput = new List<PaymentMethodGetModel>();

        foreach (var paymentMethod in paymentMethods)
        {
            var paymentMethodOutput = _mapper.Map<PaymentMethodGetModel>(paymentMethod);
            paymentMethodsOutput.Add(paymentMethodOutput);
        }

        return Ok(paymentMethodsOutput);
    }

}
