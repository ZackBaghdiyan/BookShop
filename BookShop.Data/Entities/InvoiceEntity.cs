﻿using BookShop.Data.Abstractions;

namespace BookShop.Data.Entities;

public class InvoiceEntity : IIdentifiable
{
    public long Id { get; set; }
    public long ClientId { get; set; }
    public long PaymentId { get; set; }
    public long OrderId { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public decimal TotalAmount { get; set; }
    public bool IsPaid { get; set; }
    public PaymentEntity? Payment { get; set; }
    public OrderEntity? Order { get; set; }
    public ClientEntity? Client { get; set; }
}