using BookShop.Services.Abstractions;
using BookShop.Services.Implementations;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAllServices(this IServiceCollection services)
    {
        services.AddScoped<IClientService, ClientService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICustomAuthenticationService, CustomAuthenticationService>();
        services.AddScoped<ICartService, CartService>();
        services.AddScoped<ICartItemService, CartItemService>();
        services.AddScoped<IWishListItemService, WishListItemService>();
        services.AddScoped<IWishListService, WishListService>();
        services.AddScoped<IPaymentMethodService, PaymentMethodService>();
        services.AddScoped<IPaymentService, PaymentService>();

        return services;
    }
}
