using BookShop.Common.ClientService.Abstractions;
using BookShop.Common.ClientService.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace BookShop.Common.ClientService;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddClientContext(this IServiceCollection services)
    {
        services.AddScoped<ClientContext>();
        services.AddScoped<IClientContextWriter, ClientContextWriter>();
        services.AddScoped<IClientContextReader, ClientContextReader>();

        return services;
    }
}
