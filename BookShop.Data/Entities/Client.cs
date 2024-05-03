using BookShop.Data.Abstractions;

namespace BookShop.Data.Entities;

public class Client : IIdentifiable
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Email { get; set; } = null!;

    public List<PaymentMethod>? PaymentMethods { get; set; }
    public List<WishList>? WishLists { get; set; }
    public List<Cart>? Carts { get; set; }
    public List<Order>? Orders { get; set; }
    public List<Invoice>? Invoices { get; set; }
}
