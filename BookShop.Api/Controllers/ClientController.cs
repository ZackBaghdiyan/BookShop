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

    [HttpDelete("{id}")]
    public async Task<ActionResult> RemoveClientById(long id)
    {
        await _clientService.RemoveByIdAsync(id);

        return Ok();
    }

    [HttpPut]
    public async Task<ActionResult> ModifyClient(ClientPutModel clientInput)
    {
        var client = _mapper.Map<Client>(clientInput);

        await _clientService.ModifyAsync(client);

        return Ok();
    }

    [HttpPost]
    public async Task<ActionResult> AddClient(ClientPostModel clientInput)
    {
        var client = _mapper.Map<Client>(clientInput);
        await _clientService.AddAsync(client);

        return Ok();
    }
}
