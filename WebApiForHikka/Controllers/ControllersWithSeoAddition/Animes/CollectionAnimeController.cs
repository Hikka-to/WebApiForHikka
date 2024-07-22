using AutoMapper;
using WebApiForHikka.Application.Relation.CollectionAnimes;
using WebApiForHikka.Domain.Models.Relation;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.WebApi.Shared.RelationController;

namespace WebApiForHikka.WebApi.Controllers.ControllersWithSeoAddition.Animes;

public class CollectionAnimeController(
    ICollectionAnimeRelationService relationService,
    IMapper mapper,
    IHttpContextAccessor httpContextAccessor) : RelationCrudController<
    CollectionAnime,
    Collection,
    Anime,
    ICollectionAnimeRelationService
>(relationService, mapper, httpContextAccessor);