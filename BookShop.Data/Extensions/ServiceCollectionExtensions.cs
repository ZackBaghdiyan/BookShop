using BookShop.Data.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BookShop.Data.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBookShopDbContext(this IServiceCollection services, DbOptions dbOption)
    {
        services.AddDbContext<BookShopDbContext>(b =>
        {
            b.UseNpgsql(dbOption.ConnectionString);
        });

        return services;
    }
}

