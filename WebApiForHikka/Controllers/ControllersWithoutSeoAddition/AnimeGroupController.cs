using AutoMapper;
using WebApiForHikka.Application.WithoutSeoAddition.AnimeGroups;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Dtos.Dto.WithoutSeoAddition.AnimeGroups;
using WebApiForHikka.WebApi.Shared;

namespace WebApiForHikka.WebApi.Controllers.ControllersWithoutSeoAddition;

public class AnimeGroupController(
    IAnimeGroupService crudService,
    IMapper mapper,
    IHttpContextAccessor httpContextAccessor)
    : CrudController<
        GetAnimeGroupDto,
        UpdateAnimeGroupDto,
        CreateAnimeGroupDto,
        IAnimeGroupService,
        AnimeGroup
    >(crudService, mapper, httpContextAccessor);