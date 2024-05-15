using BookShop.Api.MiddleWares;
using BookShop.Api.Services.Implementations;
using BookShop.Services.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace BookShop.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "BookShop.Api", Version = "v1" });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Query,
                Description = "Enter your token for authentication",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
            });
        });

        return services;
    }

    public static IServiceCollection ConfigureJwt(this IServiceCollection services, JwtOptions jwtOptions)
    {
        var key = Encoding.ASCII.GetBytes(jwtOptions.SecretKey!);
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtOptions.Issuer,
                    ValidAudience = jwtOptions.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                };
            });

        return services;
    }

    public static IServiceCollection AddGlobalExceptionHandler(this IServiceCollection services)
    {
        services.AddTransient<GlobalExceptionHandler>();

        return services;
    }
    
    public static IServiceCollection AddClientContextMiddleware(this IServiceCollection services)
    {
        services.AddTransient<ClientContextMiddleware>();

        return services;
    }
    
    public static IServiceCollection AddDatabaseMigrationService(this IServiceCollection services)
    {
        services.AddHostedService<DatabaseMigrationService>();

        return services;
    }
}
