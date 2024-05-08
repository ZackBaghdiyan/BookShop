using AutoMapper;
using BookShop.Api.Models.ClientModels;
using BookShop.Data.Entities;
using BookShop.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ClientController : ControllerBase
{
    private readonly IClientService _clientService;
    private readonly IMapper _mapper;

    public ClientController(IClientService clientService, IMapper mapper)
    {
        _clientService = clientService;
        _mapper = mapper;
    }

    [HttpDelete]
    public async Task<ActionResult> RemoveClient([FromBody] ClientDELETEModel clientInput)
    {
        var client = _mapper.Map<ClientEntity>(clientInput);

        await _clientService.RemoveAsync(client);

        return Ok();
    }

    [HttpPut]
    public async Task<ActionResult> UpdateClient(ClientPUTModel clientInput)
    {
        var client = _mapper.Map<ClientEntity>(clientInput);

        await _clientService.UpdateAsync(client);

        return Ok();
    }

    [HttpPost]
    public async Task<ActionResult> RegisterClient(ClientPOSTModel clientInput)
    {
        var client = _mapper.Map<ClientEntity>(clientInput);
        await _clientService.RegisterAsync(client);

        return Ok();
    }
}
