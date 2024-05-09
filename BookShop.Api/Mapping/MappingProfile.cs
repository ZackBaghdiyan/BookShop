using AutoMapper;
using BookShop.Api.Models.ClientModels;
using BookShop.Data.Entities;

namespace BookShop.Api.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ClientPostModel, ClientEntity>();
        CreateMap<ClientEntity, ClientPostModel>();
        CreateMap<ClientPutModel, ClientEntity>();
        CreateMap<ClientEntity, ClientPutModel>();
    }
}
