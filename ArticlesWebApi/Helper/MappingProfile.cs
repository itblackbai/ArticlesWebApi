using ArticlesWebApi.Dto;
using ArticlesWebApi.Models;
using AutoMapper;

namespace ArticlesWebApi.Helper
{
    public class MappingProfile : Profile
    {
         public MappingProfile() 
         {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
         }
    }
}
