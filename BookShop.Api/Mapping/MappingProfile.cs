using AutoMapper;
using BookShop.Api.Models.ClientModels;
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
    }
}
