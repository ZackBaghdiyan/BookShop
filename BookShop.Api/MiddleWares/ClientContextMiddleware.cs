using BookShop.Api.Attributes;
using BookShop.Common.ClientService;
using BookShop.Common.Consts;
using System.IdentityModel.Tokens.Jwt;

namespace BookShop.Api.MiddleWares;

public class ClientContextMiddleware : IMiddleware
{
    private readonly ClientContextAccessor _clientContextAccessor;

    public ClientContextMiddleware(ClientContextAccessor clientContextAccessor)
    {
        _clientContextAccessor = clientContextAccessor;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var excludeClientContext = context.GetEndpoint()?.Metadata.GetMetadata<ExcludeFromClientContextMiddleware>() != null;

        if (!excludeClientContext)
        {
            var tokenHeader = context.Request.Headers["Authorization"].ToString();

            if (context.Request.Path == "/Client/register" || context.Request.Path == "/Authentication/login") { }
            else if (string.IsNullOrEmpty(tokenHeader))
            {
                throw new Exception("Token is missing");
            }
            else
            {
                var token = tokenHeader.Replace("Bearer ", string.Empty);

                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.ReadJwtToken(token);

                var clientIdClaim = securityToken.Claims.FirstOrDefault(c => c.Type == BookShopClaims.Id);

                if (clientIdClaim == null)
                {
                    throw new Exception("clientId is missing");
                }

                if (!long.TryParse(clientIdClaim.Value, out long clientId))
                {
                    throw new Exception("Unknown clientId");
                }

                _clientContextAccessor.SetClientContextId(clientId);
            }
        }

        await next(context);
    }
}
