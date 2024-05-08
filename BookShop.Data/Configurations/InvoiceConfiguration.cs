using BookShop.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookShop.Data.Configurations;

public class InvoiceConfiguration : IEntityTypeConfiguration<InvoiceEntity>
{
    public void Configure(EntityTypeBuilder<InvoiceEntity> builder)
    {
        builder.HasKey(i => i.Id);

        builder.HasOne(i => i.Client)
               .WithMany(c => c.Invoices)
               .HasForeignKey(i => i.ClientId);

        builder.HasOne(i => i.Order)
               .WithOne(o => o.Invoice)
               .HasForeignKey<InvoiceEntity>(i => i.OrderId);

        builder.HasOne(i => i.Payment)
               .WithOne(p => p.Invoice)
               .HasForeignKey<InvoiceEntity>(i => i.PaymentId);
    }
}
