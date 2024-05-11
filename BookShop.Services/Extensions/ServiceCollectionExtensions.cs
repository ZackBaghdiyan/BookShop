using BookShop.Services.Abstractions;
using BookShop.Services.Implementations;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAllServices(this IServiceCollection services)
    {
        services.AddTransient<IClientService, ClientService>();
        services.AddTransient<IProductService, ProductService>();
        services.AddTransient<ICustomAuthenticationService, CustomAuthenticationService>();
        services.AddTransient<ICartService, CartService>();
        services.AddTransient<ICartItemService, CartItemService>();
        services.AddTransient<IWishListItemService, WishListItemService>();
        services.AddTransient<IWishListService, WishListService>();
        services.AddTransient<IPaymentMethodService, PaymentMethodService>();
        services.AddTransient<IPaymentService, PaymentService>();

        return services;
    }
}
