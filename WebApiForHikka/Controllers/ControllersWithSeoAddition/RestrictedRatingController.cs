using AutoMapper;
using WebApiForHikka.Application.SeoAdditions;
using WebApiForHikka.Application.RestrictedRatings;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Dtos.Dto.RestrictedRatings;
using WebApiForHikka.WebApi.Shared;

namespace WebApiForHikka.WebApi.Controllers.ControllersWithSeoAddition;

public class RestrictedRatingController : CrudControllerForModelWithSeoAddition<
    GetRestrictedRatingDto,
    UpdateRestrictedRatingDto,
    CreateRestrictedRatingDto,
   IRestrictedRatingService,
   RestrictedRating
    >
{
    public RestrictedRatingController(IRestrictedRatingService crudService, ISeoAdditionService seoAdditionService, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(crudService, seoAdditionService, mapper, httpContextAccessor)
    {
    }
}
