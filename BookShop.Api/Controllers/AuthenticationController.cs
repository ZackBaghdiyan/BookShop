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

    public AuthenticationController(ICustomAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost]
    [Route("login")]
    public async Task<ActionResult<TokenLoginVm>> Login(ClientLoginVm clientLoginVm)
    {
        var clientEntity = await _authenticationService.AuthenticateAsync(clientLoginVm.Email, clientLoginVm.Password);
        var tokenLoginVm = new TokenLoginVm();

        if (clientEntity != null)
        {
            var token = _authenticationService.GenerateToken(clientLoginVm.Email);
            tokenLoginVm.Token = token;

            return Ok(tokenLoginVm);
        }

        tokenLoginVm.Token = "Invalid email or password";

        return Unauthorized(tokenLoginVm);
    }
}
