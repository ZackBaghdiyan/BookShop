using BookShop.Services.Models.ClientModels;
using BookShop.Services.Models.TokenModels;
using BookShop.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly ICustomAuthenticationService _authenticationService;
    private readonly IClientService _clientService;

    public AuthenticationController(ICustomAuthenticationService authenticationService, IClientService clientService)
    {
        _authenticationService = authenticationService;
        _clientService = clientService;
    }

    [HttpPost]
    [Route("login")]
    public async Task<ActionResult<TokenLoginModel>> Login(ClientLoginModel clientLoginModel)
    {
        var clientEntity = await _clientService.GetByEmailAndPasswordAsync(clientLoginModel.Email, clientLoginModel.Password);

        if (clientEntity == null)
        {
            return Unauthorized();
        }

        var token = _authenticationService.GenerateToken(clientEntity);

        var tokenModel = new TokenLoginModel { Token = token };

        return Ok(tokenModel);
    }
}
