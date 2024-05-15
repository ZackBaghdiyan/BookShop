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
               .WithMany(p => p.CartItemEntity)
               .HasForeignKey(ci => ci.ProductId);

        builder.HasOne(ci => ci.CartEntity)
               .WithMany(c => c.CartItems)
               .HasForeignKey(ci => ci.CartId);

        builder.HasIndex(ci => new { ci.ProductId, ci.CartId }).IsUnique();
    }
}
