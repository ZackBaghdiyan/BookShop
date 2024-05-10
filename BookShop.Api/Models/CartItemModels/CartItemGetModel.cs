﻿using BookShop.Data.Abstractions;

namespace BookShop.Api.Models.CartItemModels;

public class CartItemGetModel : IIdentifiable
{
    public long Id { get; set; }
    public long ProductId { get; set; }
    public long CartId { get; set; }
    public long Count { get; set; }
    public decimal Price { get; set; }
}
