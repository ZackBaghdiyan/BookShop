using BookShop.Data.Abstractions;

namespace BookShop.Data.Entities;

public class ClientEntity : IIdentifiable
{
    public long Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Email { get; set; } = null!;
    public CartEntity? Cart { get; set; }
    public WishListEntity? WishList { get; set; }
    public List<PaymentMethodEntity> PaymentMethods { get; set; } = new();
    public List<WishListItem> WishListItems { get; set; } = new();
    public List<CartItem> CartItems { get; set; } = new();
    public List<OrderEntity> Orders { get; set; } = new();
    public List<InvoiceEntity> Invoices { get; set; } = new();
}
