﻿using BookShop.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookShop.Data.Configurations;

public class WishListConfiguration : IEntityTypeConfiguration<WishListEntity>
{
    public void Configure(EntityTypeBuilder<WishListEntity> builder)
    {
        builder.HasKey(wl => wl.Id);

        builder.HasOne(wl => wl.ClientEntity)
               .WithOne(c => c.WishListEntity)
               .HasForeignKey<WishListEntity>(wl => wl.ClientId);
    }
}
