﻿using BookShop.Data.Abstractions;

namespace BookShop.Data.Entities;

public class Product : IIdentifiable
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public string Manufacturer { get; set; } = null!;
    public string Details { get; set; } = null!;
    public int Count { get; set; }
    public List<Order> Orders { get; set; } = new();
    public List<WishListItem> WishLists { get; set; } = new();
    public List<CartItem> Carts { get; set; } = new();
}