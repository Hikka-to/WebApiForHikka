using AutoMapper;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.WebApi.Dto.Users;

namespace WebApiForHikka.WebApi.Helper;
public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        //User
        CreateMap<User, GetUserDto>();

        CreateMap<User, UpdateUserDto>();

        CreateMap<UpdateUserDto, User>();

    }
}
