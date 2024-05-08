using BookShop.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookShop.Data.Configurations;

public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
{
    public void Configure(EntityTypeBuilder<CartItem> builder)
    {
        builder.HasKey(ci => ci.Id);

        builder.HasOne(ci => ci.Product)
               .WithOne(p => p.CartItem)
               .HasForeignKey<CartItem>(ci => ci.ProductId);

        builder.HasOne(ci => ci.Cart)
               .WithMany(c => c.CartItems)
               .HasForeignKey(ci => ci.CartId);
    }
}
