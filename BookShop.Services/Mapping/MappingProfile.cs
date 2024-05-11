using AutoMapper;
using BookShop.Services.Models.CartItemModels;
using BookShop.Services.Models.CartModels;
using BookShop.Services.Models.ClientModels;
using BookShop.Services.Models.PaymentMethodModels;
using BookShop.Services.Models.ProductModels;
using BookShop.Services.Models.WishListItemModels;
using BookShop.Services.Models.WishListModels;
using BookShop.Data.Entities;

namespace BookShop.Services.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ClientRegisterVm, ClientEntity>();
        CreateMap<ClientUpdateVm, ClientEntity>();
        CreateMap<ClientLoginVm, ClientEntity>();
        CreateMap<ClientEntity, ClientGetVm>();
        CreateMap<ClientEntity, ClientTokenVm>();

        CreateMap<ProductUpdateVm, ProductEntity>();
        CreateMap<ProductAddVm, ProductEntity>();
        CreateMap<ProductEntity, ProductGetVm>();

        CreateMap<CartCreateVm, CartEntity>();
        CreateMap<CartEntity, CartGetVm>();

        CreateMap<CartItemEntity, CartItemGetVm>();
        CreateMap<CartItemAddVm, CartItemEntity>();
        CreateMap<CartItemUpdateVm, CartItemEntity>();

        CreateMap<WishListCreateVm, WishListEntity>();
        CreateMap<WishListEntity, WishListGetVm>();

        CreateMap<WishListItemEntity, WishListItemGetVm>();
        CreateMap<WishListItemAddVm, WishListItemEntity>();
    }
}
