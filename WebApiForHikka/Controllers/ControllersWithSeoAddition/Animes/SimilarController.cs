using AutoMapper;
using WebApiForHikka.Application.Relation.Similars;
using WebApiForHikka.Domain.Models.Relation;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.WebApi.Shared.RelationController;

namespace WebApiForHikka.WebApi.Controllers.ControllersWithSeoAddition.Animes;

public class SimilarController(
    ISimilarRelationService relationService,
    IMapper mapper,
    IHttpContextAccessor httpContextAccessor)
    : RelationCrudController<
        Similar,
        Anime,
        Anime,
        ISimilarRelationService
    >(relationService, mapper, httpContextAccessor);