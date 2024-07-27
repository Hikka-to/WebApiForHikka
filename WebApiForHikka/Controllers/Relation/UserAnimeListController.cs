using AutoMapper;
using WebApiForHikka.Application.Relation.UserAnimeLists;
using WebApiForHikka.Domain.Models.Relation;
using WebApiForHikka.Dtos.Dto.Relation.UserAnimeLists;
using WebApiForHikka.WebApi.Shared;

namespace WebApiForHikka.WebApi.Controllers.Relation;

public class UserAnimeListController(
    IUserAnimeListRelationService crudService,
    IMapper mapper,
    IHttpContextAccessor httpContextAccessor)
    : CrudController<
        GetUserAnimeListDto,
        UpdateUserAnimeListDto,
        CreateUserAnimeListDto,
        IUserAnimeListRelationService,
        UserAnimeList
    >(crudService, mapper, httpContextAccessor);