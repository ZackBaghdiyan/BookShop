﻿using BookShop.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookShop.Data.Configurations;

public class InvoiceConfiguration : IEntityTypeConfiguration<InvoiceEntity>
{
    public void Configure(EntityTypeBuilder<InvoiceEntity> builder)
    {
        builder.HasKey(i => i.Id);

        builder.HasOne(i => i.ClientEntity)
               .WithMany(c => c.Invoices)
               .HasForeignKey(i => i.ClientId);

        builder.HasOne(i => i.OrderEntity)
               .WithOne(o => o.InvoiceEntity)
               .HasForeignKey<InvoiceEntity>(i => i.OrderId);

        builder.HasOne(i => i.PaymentEntity)
               .WithOne(p => p.InvoiceEntity)
               .HasForeignKey<InvoiceEntity>(i => i.PaymentId);
    }
}
