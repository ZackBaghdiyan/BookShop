using AutoMapper;
using BookShop.Api.Models.ClientModels;
using BookShop.Data.Entities;

namespace Uni.Api.Mapping;

public class AppMappingProfile : Profile
{
    public AppMappingProfile()
    {
        CreateMap<ClientPostModel, ClientEntity>();
        CreateMap<ClientEntity, ClientPostModel>();
        CreateMap<ClientPutModel, ClientEntity>();
        CreateMap<ClientEntity, ClientPutModel>();
    }
}
