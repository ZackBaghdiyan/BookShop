using BookShop.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookShop.Data.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);

        builder
            .HasMany(p => p.WishLists)
            .WithOne(wl => wl.Product)
            .HasForeignKey(wl => wl.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(p => p.Carts)
            .WithOne(cart => cart.Product)
            .HasForeignKey(cart => cart.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(p => p.Orders)
            .WithOne(order => order.Product)
            .HasForeignKey(order => order.ProductId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}