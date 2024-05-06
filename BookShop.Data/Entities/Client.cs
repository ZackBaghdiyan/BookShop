using BookShop.Data.Abstractions;

namespace BookShop.Data.Entities;

public class Client : IIdentifiable
{
    public long Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Email { get; set; } = null!;
    public List<PaymentMethod> PaymentMethods { get; set; } = new();
    public List<WishListItem> WishLists { get; set; } = new();
    public List<CartItem> CartItems { get; set; } = new();
    public List<Order> Orders { get; set; } = new();
    public List<Invoice> Invoices { get; set; } = new();
}
