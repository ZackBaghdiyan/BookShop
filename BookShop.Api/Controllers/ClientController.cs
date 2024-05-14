using BookShop.Services.Models.ClientModels;
using BookShop.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BookShop.Api.Attributes;

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
    public async Task<IActionResult> RemoveClient()
    {
        await _clientService.RemoveAsync();

        return Ok();
    }

    [HttpPut]
    public async Task<ActionResult<ClientModel>> UpdateClient(ClientUpdateModel clientInput)
    {
        var clientOutput = await _clientService.UpdateAsync(clientInput);

        return Ok(clientOutput);
    }

    [ExcludeFromClientContextMiddleware]
    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<ActionResult<ClientModel>> RegisterClient(ClientRegisterModel clientRegisterModel)
    {
        var client = await _clientService.RegisterAsync(clientRegisterModel);

        return Ok(client);
    }
}
