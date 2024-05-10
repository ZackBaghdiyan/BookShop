using AutoMapper;
using BookShop.Api.Models.CartItemModels;
using BookShop.Api.Models.CartModels;
using BookShop.Api.Models.ClientModels;
using BookShop.Api.Models.ProductModels;
using BookShop.Data.Entities;

namespace BookShop.Api.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ClientPostModel, ClientEntity>();
        CreateMap<ClientPutModel, ClientEntity>();
        CreateMap<ClientLoginModel, ClientEntity>();

        CreateMap<ProductPutModel, ProductEntity>();
        CreateMap<ProductPostModel, ProductEntity>();
        CreateMap<ProductEntity, ProductGetModel>();

        CreateMap<CartPostModel, CartEntity>();

        CreateMap<CartItemEntity, CartItemGetModel>();
    }
}
