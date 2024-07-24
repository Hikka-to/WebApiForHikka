using AutoMapper;
using WebApiForHikka.Application.SeoAdditions;
using WebApiForHikka.Application.WithSeoAddition.RestrictedRatings;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.Dtos.Dto.WithSeoAddition.RestrictedRatings;
using WebApiForHikka.WebApi.Shared;

namespace WebApiForHikka.WebApi.Controllers.ControllersWithSeoAddition;

public class RestrictedRatingController(
    IRestrictedRatingService crudService,
    ISeoAdditionService seoAdditionService,
    IMapper mapper,
    IHttpContextAccessor httpContextAccessor)
    : CrudControllerForModelWithSeoAddition<
        GetRestrictedRatingDto,
        UpdateRestrictedRatingDto,
        CreateRestrictedRatingDto,
        IRestrictedRatingService,
        RestrictedRating
    >(crudService, seoAdditionService, mapper, httpContextAccessor);