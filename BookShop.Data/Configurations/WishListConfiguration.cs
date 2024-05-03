using BookShop.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookShop.Data.Configurations;

public class WishListConfiguration : IEntityTypeConfiguration<WishList>
{
    public void Configure(EntityTypeBuilder<WishList> builder)
    {
        builder.HasKey(w => w.Id);

        builder
            .HasIndex(wl => new { wl.ClientId, wl.ProductId });

        builder
            .HasOne(wl => wl.Client)
            .WithMany(c => c.WishLists)
            .HasForeignKey(wl => wl.ClientId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(wl => wl.Product)
            .WithMany(p => p.WishLists)
            .HasForeignKey(wl => wl.ProductId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
