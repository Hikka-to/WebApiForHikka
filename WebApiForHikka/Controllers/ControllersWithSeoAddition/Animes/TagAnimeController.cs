using AutoMapper;
using WebApiForHikka.Application.Relation.TagAnimes;
using WebApiForHikka.Domain.Models.Relation;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.WebApi.Shared.RelationController;

namespace WebApiForHikka.WebApi.Controllers.ControllersWithSeoAddition.Animes;

public class TagAnimeController(
    ITagAnimeRelationService relationService,
    IMapper mapper,
    IHttpContextAccessor httpContextAccessor) : RelationCrudController<
    TagAnime,
    Tag,
    Anime,
    ITagAnimeRelationService
>(relationService, mapper, httpContextAccessor);