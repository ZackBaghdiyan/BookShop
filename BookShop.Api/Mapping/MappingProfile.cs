using AutoMapper;
using BookShop.Api.Models.ClientModels;
using BookShop.Api.Models.ProductModels;
using BookShop.Data.Entities;

namespace BookShop.Api.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ClientPOSTModel, ClientEntity>();
        CreateMap<ClientEntity, ClientPOSTModel>();
        CreateMap<ClientPUTModel, ClientEntity>();
        CreateMap<ClientEntity, ClientPUTModel>();
        CreateMap<ClientDELETEModel, ClientEntity>();
        CreateMap<ClientEntity, ClientDELETEModel>();

        CreateMap<ProductPOSTModel, ProductEntity>();
        CreateMap<ProductEntity, ProductPOSTModel>();
        CreateMap<ProductPUTModel, ProductEntity>();
        CreateMap<ProductEntity, ProductPUTModel>();
        CreateMap<ProductDELETEModel, ProductEntity>();
        CreateMap<ProductEntity, ProductDELETEModel>();
    }
}
