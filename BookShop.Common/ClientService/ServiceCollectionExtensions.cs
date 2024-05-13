using Microsoft.Extensions.DependencyInjection;

namespace BookShop.Common.ClientService;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddClientContext(this IServiceCollection services)
    {
        services.AddScoped<ClientContext>();
        services.AddScoped<ClientContextAccessor>();
        services.AddScoped<ClientContextReader>();

        return services;
    }
}
