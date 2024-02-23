using AutoMapper;
using Management.Service.DTOs;
using Management.Domain.Entities;

namespace Management.Service.Mappers;
public class MapperProfile:Profile
{
    public MapperProfile()
    {
        CreateMap<User, UserPostModel>().ReverseMap();
        CreateMap<User,UserPutModel>().ReverseMap();
        CreateMap<User,UserViewModel>().ReverseMap();
    }
}

