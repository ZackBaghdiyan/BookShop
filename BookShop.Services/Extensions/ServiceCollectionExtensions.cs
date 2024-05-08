using BookShop.Services.Abstractions;
using BookShop.Services.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace BookShop.Services.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAllServices(this IServiceCollection services)
    {
        services.AddScoped<IClientService, ClientService>();

        return services;
    }
}
