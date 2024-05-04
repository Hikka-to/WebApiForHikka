using AutoMapper;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Dtos.Dto.Users;

namespace WebApiForHikka.Dtos.Helper;
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
