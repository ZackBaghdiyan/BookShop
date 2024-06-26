﻿using AutoMapper;
using BookShop.Services.Models.CartItemModels;
using BookShop.Services.Models.ClientModels;
using BookShop.Services.Models.ProductModels;
using BookShop.Services.Models.WishListItemModels;
using BookShop.Data.Entities;

namespace BookShop.Services.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ClientRegisterModel, ClientEntity>();
        CreateMap<ClientUpdateModel, ClientEntity>();
        CreateMap<ClientLoginModel, ClientEntity>();
        CreateMap<ClientEntity, ClientModel>();

        CreateMap<ProductUpdateModel, ProductEntity>();
        CreateMap<ProductAddModel, ProductEntity>();
        CreateMap<ProductEntity, ProductModel>();

        CreateMap<CartItemEntity, CartItemModel>();
        CreateMap<CartItemAddModel, CartItemEntity>();
        CreateMap<CartItemUpdateModel, CartItemEntity>();

        CreateMap<WishListItemEntity, WishListItemModel>();
        CreateMap<WishListItemAddModel, WishListItemEntity>();
    }
}
