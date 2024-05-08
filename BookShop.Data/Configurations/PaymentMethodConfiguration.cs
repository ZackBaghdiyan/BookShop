﻿using BookShop.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookShop.Data.Configurations;

public class PaymentMethodConfiguration : IEntityTypeConfiguration<PaymentMethodEntity>
{
    public void Configure(EntityTypeBuilder<PaymentMethodEntity> builder)
    {
        builder.HasKey(pm => pm.Id);

        builder.HasOne(pm => pm.Client)
               .WithMany(c => c.PaymentMethods)
               .HasForeignKey(pm => pm.ClientId);
    }
}
