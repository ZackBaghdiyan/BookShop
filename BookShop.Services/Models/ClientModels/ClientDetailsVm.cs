namespace BookShop.Services.Models.ClientModels;

public class ClientDetailsVm
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string Email { get; set; } = null!;
    public long CartId { get; set; }
    public long WishListId { get; set; }
    public long ClientId { get; set; }
}
