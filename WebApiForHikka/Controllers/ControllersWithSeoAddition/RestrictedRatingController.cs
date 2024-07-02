using AutoMapper;
using WebApiForHikka.Application.RestrictedRatings;
using WebApiForHikka.Application.SeoAdditions;
using WebApiForHikka.Constants.Models.RestrictedRatings;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Dtos.Dto.RestrictedRatings;
using WebApiForHikka.WebApi.Shared;

namespace WebApiForHikka.WebApi.Controllers.ControllersWithSeoAddition;

public class RestrictedRatingController
    (IRestrictedRatingService crudService, ISeoAdditionService seoAdditionService, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    : CrudControllerForModelWithSeoAddition<
        GetRestrictedRatingDto,
        UpdateRestrictedRatingDto,
        CreateRestrictedRatingDto,
        IRestrictedRatingService,
        RestrictedRating
    >(crudService, seoAdditionService, mapper, httpContextAccessor);