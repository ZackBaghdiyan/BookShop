using BookShop.Data.Options;
using BookShop.Services.Options;

namespace BookShop.Api.Extensions;

public static class ConfigurationExtensions
{
    public static DbOptions ConfigureDbOptions(this IConfiguration configuration)
    {
        var connString = configuration.GetSection($"{DbOptions.SectionName}:{nameof(DbOptions.ConnectionString)}").Value;

        return new DbOptions { ConnectionString = connString };
    }

    public static JwtOptions GetJwtOptions(this IConfiguration configuration)
    {
        var secretKey = configuration.GetSection($"{JwtOptions.SectionName}:{nameof(JwtOptions.SecretKey)}");
        var issuer = configuration.GetSection($"{JwtOptions.SectionName}:{nameof(JwtOptions.Issuer)}");
        var audience = configuration.GetSection($"{JwtOptions.SectionName}:{nameof(JwtOptions.Audience)}");

        return new JwtOptions
        {
            SecretKey = secretKey.Value,
            Issuer = issuer.Value,
            Audience = audience.Value
        };
    }
}
