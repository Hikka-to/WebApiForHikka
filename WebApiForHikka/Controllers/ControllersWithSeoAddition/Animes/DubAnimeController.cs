using AutoMapper;
using WebApiForHikka.Application.Relation.DubAnimes;
using WebApiForHikka.Domain.Models.Relation;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.WebApi.Shared.RelationController;

namespace WebApiForHikka.WebApi.Controllers.ControllersWithSeoAddition.Animes;

public class DubAnimeController(
    IDubAnimeRelationService relationService,
    IMapper mapper,
    IHttpContextAccessor httpContextAccessor) : RelationCrudController<
    DubAnime,
    Dub,
    Anime,
    IDubAnimeRelationService
>(relationService, mapper, httpContextAccessor);