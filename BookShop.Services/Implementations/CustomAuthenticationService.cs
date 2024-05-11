using AutoMapper;
using BookShop.Data;
using BookShop.Services.Abstractions;
using BookShop.Services.Models.ClientModels;
using BookShop.Services.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BookShop.Services.Implementations;

public class CustomAuthenticationService : ICustomAuthenticationService
{
    private readonly JwtOptions _jwtOptions;
    private readonly BookShopDbContext _dbContext;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly IMapper _mapper;

    public CustomAuthenticationService(JwtOptions jwtOptions, BookShopDbContext dbContext,
        IHttpContextAccessor contextAccessor, IMapper mapper)
    {
        _jwtOptions = jwtOptions;
        _dbContext = dbContext;
        _contextAccessor = contextAccessor;
        _mapper = mapper;
    }

    public string GenerateToken(string clientEmail)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, clientEmail),
        };

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwtOptions.Issuer,
            audience: _jwtOptions.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(10),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<ClientTokenVm?> AuthenticateAsync(string email, string password)
    {
        var client = await _dbContext.Clients.FirstOrDefaultAsync(c => c.Email == email);

        if (client == null || !VerifyPasswordHash(password, client.Password))
        {
            return null;
        }

        var clientTokenVm = _mapper.Map<ClientTokenVm>(client);

        return clientTokenVm;
    }

    private bool VerifyPasswordHash(string password, string storedHash)
    {
        using (var sha256 = SHA256.Create())
        {
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            var hashedPassword = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();

            return string.Equals(hashedPassword, storedHash);
        }
    }

    public string GetClientEmailFromToken()
    {
        var token = _contextAccessor.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (token != null)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtOptions.SecretKey);

            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var clientEmail = jwtToken.Claims.First(x => x.Type == ClaimTypes.Email).Value;

            return clientEmail;
        }

        throw new InvalidOperationException("Token not found.");
    }
}
