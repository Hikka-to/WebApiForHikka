using AutoMapper;
using WebApiForHikka.Application.Relation.CountryAnimes;
using WebApiForHikka.Domain.Models.Relation;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.WebApi.Shared.RelationController;

namespace WebApiForHikka.WebApi.Controllers.ControllersWithSeoAddition.Animes;

public class CountryAnimeController(
    ICountryAnimeRelationService relationService,
    IMapper mapper,
    IHttpContextAccessor httpContextAccessor) : RelationCrudController<
    CountryAnime,
    Country,
    Anime,
    ICountryAnimeRelationService
>(relationService, mapper, httpContextAccessor);