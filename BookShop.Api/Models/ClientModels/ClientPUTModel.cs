﻿namespace BookShop.Api.Models.ClientModels;

public class ClientPUTModel
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}