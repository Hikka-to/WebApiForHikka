using AutoMapper;
using WebApiForHikka.Application.Relation.AnimeRatings;
using WebApiForHikka.Domain.Models.Relation;
using WebApiForHikka.Dtos.Dto.Relation.AnimeRatings;
using WebApiForHikka.WebApi.Shared;

namespace WebApiForHikka.WebApi.Controllers.Relation;

public class AnimeRatingController(
    IAnimeRatingRelationService crudRelationService,
    IMapper mapper,
    IHttpContextAccessor httpContextAccessor)
    : CrudController<
        GetAnimeRatingDto,
        UpdateAnimeRatingDto,
        CreateAnimeRatingDto,
        IAnimeRatingRelationService,
        AnimeRating
    >(crudRelationService, mapper, httpContextAccessor);