using AutoMapper;
using BookShop.Api.Models.ClientModels;
using BookShop.Data.Entities;
using BookShop.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ClientController : ControllerBase
{
    private readonly IClientService _service;
    private readonly IMapper _mapper;

    public ClientController(IClientService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [Authorize]
    [HttpDelete]
    public async Task<ActionResult<ClientEntity>> RemoveClient(ClientDeleteModel clientInput)
    {
        var clientToRemove = _mapper.Map<ClientEntity>(clientInput);
        await _service.RemoveAsync(clientToRemove);

        return Ok();
    }

    [Authorize]
    [HttpPut]
    public async Task<ActionResult<ClientEntity>> UpdateClient(ClientPutModel clientInput)
    {
        var client = _mapper.Map<ClientEntity>(clientInput);
        await _service.UpdateAsync(client);

        return Ok();
    }

    [HttpPost]
    public async Task<ActionResult<ClientEntity>> RegisterClient(ClientPostModel clientInput)
    {
        var client = _mapper.Map<ClientEntity>(clientInput);
        await _service.RegisterAsync(client);

        return Ok();
    }
}
