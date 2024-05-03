using BookShop.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookShop.Data.Configurations;

public class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.HasKey(c => c.Id);

        builder
            .HasMany(c => c.WishLists)
            .WithOne(wl => wl.Client)
            .HasForeignKey(wl => wl.ClientId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(c => c.Carts)
            .WithOne(cart => cart.Client)
            .HasForeignKey(cart => cart.ClientId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(c => c.Orders)
            .WithOne(order => order.Client)
            .HasForeignKey(order => order.ClientId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}
