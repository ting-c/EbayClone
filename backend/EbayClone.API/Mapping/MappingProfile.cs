using AutoMapper;
using EbayClone.API.Resources;
using EbayClone.Core.Models;

namespace EbayClone.API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {   
            // Domain to Resource
            CreateMap<Item, ItemResource>();
            CreateMap<User, UserResource>();

            //Resource to Domain
            CreateMap<ItemResource, Item>();
            CreateMap<UserResource, User>();
            CreateMap<SaveItemResource, Item>();
            CreateMap<SaveUserResource, User>();
        }
    }
}