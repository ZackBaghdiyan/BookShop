using BookShop.Services.Models.ClientModels;
using BookShop.Data.Entities;
using BookShop.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace BookShop.Api.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class ClientController : ControllerBase
{
    private readonly IClientService _clientService;

    public ClientController(IClientService clientService)
    {
        _clientService = clientService;
    }

    [HttpDelete]
    public async Task<ActionResult<ClientEntity>> RemoveClient(long clientId)
    {
        await _clientService.RemoveAsync(clientId);

        return Ok();
    }

    [HttpPut]
    public async Task<ActionResult<ClientGetVm>> UpdateClient(ClientUpdateVm clientInput)
    {
        var clientOutput = await _clientService.UpdateAsync(clientInput);

        return Ok(clientOutput);
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<ActionResult<ClientDetailsVm>> RegisterClient(ClientRegisterVm clientRegisterVm)
    {
        var (client, cart, wishList) = await _clientService.RegisterAsync(clientRegisterVm);

        var clientDetails = new ClientDetailsVm
        {
            ClientId = client.Id,
            FirstName = client.FirstName,
            LastName = client.LastName,
            Address = client.Address,
            Email = client.Email,
            CartId = cart.Id,
            WishListId = wishList.Id
        };

        return Ok(clientDetails);
    }
}
