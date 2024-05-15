﻿using BookShop.Data.Enums;
using BookShop.Data.Models;

namespace BookShop.Services.Models.PaymentMethodModels;

public class PaymentMethodAddModel
{
    public PaymentMethod PaymentMethod { get; set; }
    public CardDetails? Details { get; set; } 
}
