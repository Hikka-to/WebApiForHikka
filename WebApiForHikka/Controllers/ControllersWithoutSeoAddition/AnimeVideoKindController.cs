using AutoMapper;
using WebApiForHikka.Application.WithoutSeoAddition.AnimeVideoKinds;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Dtos.Dto.WithoutSeoAddition.AnimeVideoKinds;
using WebApiForHikka.WebApi.Shared;

namespace WebApiForHikka.WebApi.Controllers.ControllersWithoutSeoAddition;

public class AnimeVideoKindController(
    IAnimeVideoKindService crudService,
    IMapper mapper,
    IHttpContextAccessor httpContextAccessor)
    : CrudController<
        GetAnimeVideoKindDto,
        UpdateAnimeVideoKindDto,
        CreateAnimeVideoKindDto,
        IAnimeVideoKindService,
        AnimeVideoKind
    >(crudService, mapper, httpContextAccessor);