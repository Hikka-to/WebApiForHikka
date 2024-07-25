using AutoMapper;
using WebApiForHikka.Application.WithoutSeoAddition.UserAnimeListTypes;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Dtos.Dto.WithoutSeoAddition.UserAnimeListTypes;
using WebApiForHikka.WebApi.Shared;

namespace WebApiForHikka.WebApi.Controllers.ControllersWithoutSeoAddition;

public class UserAnimeListTypeController(
    UserAnimeListTypeService crudService,
    IMapper mapper,
    IHttpContextAccessor httpContextAccessor
)
    : CrudController
        <GetUserAnimeListTypeDto, UpdateUserAnimeListTypeDto, CreateUserAnimeListTypeDto, UserAnimeListTypeService, UserAnimeListType>(crudService, mapper, httpContextAccessor)
{
    
}