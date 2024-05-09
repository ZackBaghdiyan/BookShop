using BookShop.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookShop.Data.Configurations;

public class CartItemConfiguration : IEntityTypeConfiguration<CartItemEntity>
{
    public void Configure(EntityTypeBuilder<CartItemEntity> builder)
    {
        builder.HasKey(ci => ci.Id);

        builder.HasOne(ci => ci.ProductEntity)
               .WithOne(p => p.CartItemEntity)
               .HasForeignKey<CartItemEntity>(ci => ci.ProductId);

        builder.HasOne(ci => ci.CartEntity)
               .WithMany(c => c.CartItems)
               .HasForeignKey(ci => ci.CartId);
    }
}
